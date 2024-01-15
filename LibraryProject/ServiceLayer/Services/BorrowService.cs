// <copyright file="BorrowService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class BorrowService.
    /// Implements the <see cref="Services.BaseService{Borrow, IBorrowRepository}" />.
    /// Implements the <see cref="IBorrowService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{Borrow, IBorrowRepository}" />
    /// <seealso cref="IBorrowService" />
    public class BorrowService : BaseService<Borrow, IBorrowRepository>, IBorrowService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowService" /> class.
        /// </summary>
        /// <param name="borrowRepository">The borrow repository.</param>
        /// <param name="propertiesRepository">The properties repository.</param>
        private readonly IBookRepository bookRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowService" /> class.
        /// </summary>
        public BorrowService()
             : base(Injector.Create<IBorrowRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.bookRepository = Injector.Create<IBookRepository>();
            this.Validator = new BorrowValidator();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public override bool Insert(Borrow entity)
        {
            // The borrow expires after 1 month
            if (entity.BorrowDate is DateTime startDate)
            {
                entity.EndDate = startDate.AddMonths(1);
            }

            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckBorrowAdditionalRules(entity))
            {
                _ = this.Repository.Insert(entity);
            }
            else
            {
                LogUtils.LogErrors(result);
                return false;
            }

            return true;
        }

        /// <summary>
        /// A book can be borrowed if not all copies are
        /// marked as being for the reading room only.
        /// </summary>
        /// <param name="entity">Borrow.</param>
        /// <returns>bool.</returns>
        public bool CheckIfAtLeastABookIsForLecture(Borrow entity)
        {
            foreach (var bookToBeBorrowed in entity.BorrowedBooks)
            {
                var title = bookToBeBorrowed.Title;
                var allBooksWithTheSameName = BookServiceUtils.
                    GetBooksWithTheSameTitle(this.bookRepository.Get(), title);

                bool isForLecture = false;
                foreach (var book in allBooksWithTheSameName)
                {
                    if (book.LecturesOnlyBook == true)
                    {
                        isForLecture = true;
                    }
                }

                if (!isForLecture)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The number of books left (still unborrowed, but not from those for the reading room)
        /// is at least 10% of the initial fund in that book.
        /// </summary>
        /// <param name="entity">Borrow.</param>
        /// <returns>bool.</returns>
        public bool CheckNumberOfBooksLeftIsAtLeast10Percent(Borrow entity)
        {
            foreach (var bookToBeBorrowed in entity.BorrowedBooks)
            {
                var title = bookToBeBorrowed.Title;
                var allBooksWithTheSameName =
                BookServiceUtils.GetBooksWithTheSameTitle(this.bookRepository.Get(), title);
                var unavailableBooks =
                    BookServiceUtils.GetUnavailableBooks(allBooksWithTheSameName);

                var allBooksWithTheSameNameCount = allBooksWithTheSameName.Count();
                var unavailableBooksCount = unavailableBooks.Count();

                if (allBooksWithTheSameNameCount * 0.1f >=
                    allBooksWithTheSameNameCount - unavailableBooksCount)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the can borrow maximum NMC in PER.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckCanBorrowMaxNMCInPER(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var per = properties.PER;
            var nmc = properties.NMC;

            if (entity.Borrower is Librarian librarian)
            {
                if (librarian.IsReader == true)
                {
                    per /= 2;
                    nmc *= 2;
                }
            }

            // Cartile ce au fost imprumutate in ultimele PER luni
            var datePer = DateTime.Now.AddMonths((int)-per);
            var borrowsInLastPERMonths = this.Repository.Get(
                borrow => borrow.BorrowDate >= datePer,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty);
            var borrowedBooksInPERPeriod = borrowsInLastPERMonths
                .SelectMany(borrow => borrow.BorrowedBooks)
                .Distinct()
                .Count();

            if (properties.NMC <= borrowedBooksInPERPeriod + entity.BorrowedBooks.Count)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the borrowed books for maximum C books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckBorrowedBooksForMaxCBooks(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var c = properties.C;

            if (entity.Borrower is Librarian librarian)
            {
                if (librarian.IsReader == true)
                {
                    c *= 2;
                }
            }

            if (entity.BorrowedBooks.Count > c)
            {
                return false;
            }

            if (entity.BorrowedBooks.Count < 3)
            {
                return true;
            }

            int noOfDistinctDomains =
                DomainServiceUtils.GetNoOfDistinctDomains(
                    entity.BorrowedBooks
                    .SelectMany(x => x.Domains)
                    .ToList());

            return entity.BorrowedBooks.Count >= 3 && noOfDistinctDomains >= 2;
        }

        /// <summary>
        /// Checks if borrow books are at most D in last L months.
        /// D is threshold for number of domains.
        /// L is threshold for number of months.
        /// </summary>
        /// <param name="entity">The borrow.</param>
        /// <returns>bool.</returns>
        public bool CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var d = properties.D;

            if (entity.Borrower is Librarian librarian)
            {
                if (librarian.IsReader == true)
                {
                    d *= 2;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the lim.
        /// </summary>
        /// <param name="entity"> entity.</param>
        /// <returns> vrbs. </returns>
        public bool CheckBorrowExtensionAtMostLIM(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var lim = properties.LIM;

            if (entity.Borrower is Librarian librarian)
            {
                if (librarian.IsReader == true)
                {
                    lim *= 2;
                }
            }

            // Partea cu ultimele 3 luni trebuie facuta cand se face update si se doreste sa se faca extindere
            if (entity.NoOfTimeExtended >= lim)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the borrow in delta time.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckBorrowInDELTATime(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var delta = properties.DELTA;

            if (entity.Borrower is Librarian librarian)
            {
                if (librarian.IsReader == true)
                {
                    delta /= 2;
                }
            }

            var deltaBookTime = DateTime.Now.AddMonths(-(int)delta);

            // ultimul imprumut finalizat
            var borrowsWithOnlyOneBook = this.Repository.Get(
                borrow => borrow.BorrowedBooks.Count == 1,
                borrow => borrow.OrderBy(x => x.EndDate),
                string.Empty).ToList();

            var borrowsWithEndDateHigherThanDelta =
                borrowsWithOnlyOneBook
                .Where(borrow => borrow.EndDate > deltaBookTime)
                .ToList();

            var books = borrowsWithEndDateHigherThanDelta
                .SelectMany(x => x.BorrowedBooks)
                .ToList();

            var flag = books
                .Where(x => x.Id == entity.BorrowedBooks.First().Id)
                .ToList();

            return flag.Count < 1;
        }

        /// <summary>
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckCanBorrowAtMostNCZBooksToday(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var borrowsToday = this.Repository.Get(
                borrow => borrow.BorrowDate == DateTime.Today,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return borrowsToday == 0 || properties.NCZ < borrowsToday;
        }

        /// <summary>
        /// Checks borrow additional rules.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if all borrow additional rules succeed, <c>false</c> otherwise.</returns>
        public bool CheckBorrowAdditionalRules(Borrow entity)
        {
            /*if (!this.Repository.Get(null, borrow => borrow.OrderBy(x => x.Id), string.Empty).Any())
            {
                return true;
            }*/

            /* O carte poate fi imprumutata daca */
            // 1. nu are toate exemplarele marcate ca fiind doar pentru sala de lectura;
            // 2. numarul de carti ramase (inca neimprumutate, dar nu din cele
            // pentru sala de lectura) este macar 10 % din fondul initial din acea carte.
            if (this.CheckIfAtLeastABookIsForLecture(entity) == false ||
                this.CheckNumberOfBooksLeftIsAtLeast10Percent(entity) == false)
            {
                return false;
            }

            // Pot imprumuta un numar maxim de carti NMC intr-o perioada PER;
            if (this.CheckCanBorrowMaxNMCInPER(entity) == false)
            {
                return false;
            }

            // La un imprumut pot prelua cel mult C carti; daca numarul cartilor imprumutate la o
            // cerere de imprumut e cel putin 3, atunci acestea trebui sa faca parte din cel putin 2
            // categorii distincte
            if (!this.CheckBorrowedBooksForMaxCBooks(entity))
            {
                return false;
            }

            // Todo
            // Nu pot imprumuta mai mult de D carti dintr-un acelasi domeniu
            // – de tip frunza sau de nivel superior - in ultimele L luni
            if (!this.CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(entity))
            {
                return false;
            }

            // Pot imprumuta o carte pe o perioada determinata; se permit prelungiri, dar suma
            // acestor prelungiri acordate in ultimele 3 luni nu poate depasi o valoare limita LIM data
            if (!this.CheckBorrowExtensionAtMostLIM(entity))
            {
                return false;
            }

            // Nu pot imprumuta aceeasi carte de mai multe ori intr-un interval DELTA specificat, unde
            // DELTA se masoara de la ultimul imprumut al cartii
            if (!this.CheckBorrowInDELTATime(entity))
            {
                return false;
            }

            // Pot imprumuta cel mult NCZ carti intr-o zi.
            if (!this.CheckCanBorrowAtMostNCZBooksToday(entity))
            {
                return false;
            }

            foreach (var book in entity.BorrowedBooks)
            {
                book.IsBorrowed = true;
            }

            return true;
        }
    }
}
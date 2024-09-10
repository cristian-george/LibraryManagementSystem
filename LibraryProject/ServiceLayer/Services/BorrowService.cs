// <copyright file="BorrowService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
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
        private readonly IBookRepository bookRepository;
        private readonly IStockRepository stockRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowService" /> class.
        /// </summary>
        public BorrowService()
             : base(Injector.Create<IBorrowRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.bookRepository = Injector.Create<IBookRepository>();
            this.stockRepository = Injector.Create<IStockRepository>();

            this.Validator = new BorrowValidator();
        }

        /// <summary>
        /// Get the books to borrow.
        /// </summary>
        /// <param name="borrow">Borrow.</param>
        /// <param name="bookRepository">BookRepository.</param>
        /// <returns>Books.</returns>
        public IEnumerable<Book> GetBorrowedBooks(Borrow borrow, IBookRepository bookRepository)
        {
            var booksToBorrow = new List<Book>();

            foreach (var stock in borrow.Stocks)
            {
                var book = bookRepository.GetBookByStockId(stock.Id);
                booksToBorrow.Add(book);
            }

            return booksToBorrow;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public override bool Insert(Borrow entity)
        {
            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckBorrowAdditionalRules(entity))
            {
                // The borrow expires after two weeks
                if (entity.BorrowDate is DateTime borrowDate)
                {
                    entity.ReturnDate = borrowDate.AddDays(14);
                }

                // Update stocks
                foreach (var stock in entity.Stocks)
                {
                    stock.NumberOfBooksForBorrowing -= 1;
                    this.stockRepository.Update(stock);
                }

                _ = this.Repository.Insert(entity);
            }
            else
            {
                Logging.LogErrors(result);
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CheckBooks(Borrow entity)
        {
            var booksToBorrow = this.GetBorrowedBooks(entity, this.bookRepository);

            foreach (var book in booksToBorrow)
            {
                var stocks = this.stockRepository.GetStocksByBookId(book.Id);

                var initialNumberOfBooksForBorrowing = 0;
                var currentNumberOfBooksForBorrowing = 0;
                var numberOfBooksForLectureOnly = 0;

                foreach (var stock in stocks)
                {
                    initialNumberOfBooksForBorrowing += stock.InitialStock - stock.NumberOfBooksForLectureOnly;
                    currentNumberOfBooksForBorrowing += stock.NumberOfBooksForBorrowing;
                    numberOfBooksForLectureOnly += stock.NumberOfBooksForLectureOnly;
                }

                // There are no copies for a book that can be borrowed
                if (initialNumberOfBooksForBorrowing == 0)
                {
                    return false;
                }

                // There are no copies for a book that can be read in the lecture room
                if (numberOfBooksForLectureOnly == 0)
                {
                    return false;
                }

                // There is less than 10% of the initial stock of copies for a book
                if (currentNumberOfBooksForBorrowing < 0.1f * initialNumberOfBooksForBorrowing)
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
            var per = properties.Per;
            var nmc = properties.Nmc;

            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                per /= 2;
                nmc *= 2;
            }

            var datePer = DateTime.Now.AddMonths((int)-per);

            // Borrows that have been done in the last PER months
            var borrows = this.Repository.Get(
                borrow =>
                borrow.Reader.Id == entity.Reader.Id &&
                borrow.BorrowDate >= datePer);

            // Number of the books that have been borrowed in the last PER months
            var numberOfBorrowedBooks = borrows
                .SelectMany(borrow => this.GetBorrowedBooks(borrow, this.bookRepository))
                .Distinct()
                .Count();

            if (properties.Nmc <= numberOfBorrowedBooks + entity.Stocks.Count)
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

            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                c *= 2;
            }

            if (entity.Stocks.Count > c)
            {
                return false;
            }

            if (entity.Stocks.Count < 3)
            {
                return true;
            }

            var booksToBorrow = this.GetBorrowedBooks(entity, this.bookRepository);

            int numberOfDistinctDomains = booksToBorrow
                .SelectMany(x => x.Domains)
                .DistinctBy(x => x.Name)
                .Count();

            return numberOfDistinctDomains >= 2;
        }

        /// <summary>
        /// Checks if borrow books are at most D in the same domain, in the last L months.
        /// D is threshold for number of domains.
        /// L is threshold for number of months.
        /// </summary>
        /// <param name="entity">The borrow.</param>
        /// <returns>bool.</returns>
        public bool CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var d = properties.D;
            var l = properties.L;

            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                d *= 2;
            }

            // Borrows made by reader in the last L months
            var borrows = this.Repository
                .GetBorrowsByReaderId(entity.Reader.Id)
                .Where(b => b.BorrowDate >= DateTime.Now.AddMonths(-l));

            // Books that have been borrowed by reader in the last L months
            var books = borrows
                .SelectMany(borrow => this.GetBorrowedBooks(borrow, this.bookRepository))
                .Distinct()
                .ToList();

            books.AddRange(this.GetBorrowedBooks(entity, this.bookRepository));

            Dictionary<Domain, int> domains = new Dictionary<Domain, int>();

            books.ForEach(book =>
            {
                foreach (var domain in book.Domains)
                {
                    var rootDomain = domain.GetRootDomain();
                    domains[rootDomain]++;
                }
            });

            foreach (var counter in domains.Values)
            {
                if (counter > d)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if borrow extension is at most LIM.
        /// LIM is threshold for books number limit.
        /// </summary>
        /// <param name="entity">Borrow.</param>
        /// <returns>bool.</returns>
        public bool CheckBorrowExtensionAtMostLIM(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var lim = properties.Lim;

            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                lim *= 2;
            }

            var booksToBorrow = this.GetBorrowedBooks(entity, this.bookRepository);

            foreach (var book in booksToBorrow)
            {
                var bookBorrowCount = this.Repository
                    .GetBookBorrowCountByReaderWithinDate(book.Id, entity.Reader.Id, DateTime.Now.AddMonths(-3));

                if (bookBorrowCount + 1 > lim)
                {
                    return false;
                }
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
            var delta = properties.Delta;

            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                delta /= 2;
            }

            var booksToBorrow = this.GetBorrowedBooks(entity, this.bookRepository);

            foreach (var book in booksToBorrow)
            {
                var lastBorrow = this.Repository.GetLastBookBorrowByReader(book.Id, entity.Reader.Id);
                if (lastBorrow != null)
                {
                    var daysSinceLastBorrow = (DateTime.Now - lastBorrow.ReturnDate).TotalDays;
                    return daysSinceLastBorrow > delta;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckCanBorrowAtMostNCZBooksToday(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var ncz = properties.Ncz;
            if (entity.Reader.UserType == DomainLayer.Enums.EUserType.LibrarianReader)
            {
                ncz = int.MaxValue - 1;
            }

            var borrowsToday = this.Repository.Get(
                borrow => borrow.BorrowDate == DateTime.Today,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return borrowsToday == 0 || borrowsToday <= ncz;
        }

        /// <summary>
        /// Checks if librarian granted at most PERSIMP books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public bool CheckGrantAtMostPERSIMPBooksToday(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var persimp = properties.Persimp;

            var borrowsToday = this.Repository.Get(
                borrow => borrow.BorrowDate == DateTime.Today &&
                          borrow.Librarian.Equals(entity.Librarian),
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return borrowsToday <= persimp;
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
            if (this.CheckBooks(entity) == false)
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

            // Pot imprumuta o carte pe o perioada determinata; se permit prelungiri, dar suma
            // acestor prelungiri acordate in ultimele 3 luni nu poate depasi o valoare limita LIM data
            if (!this.CheckBorrowExtensionAtMostLIM(entity))
            {
                return false;
            }

            return true;
        }
    }
}
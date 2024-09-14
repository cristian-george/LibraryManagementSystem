// <copyright file="BorrowService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Enums;
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
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public override bool Insert(Borrow entity)
        {
            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckAdditionalRules(entity))
            {
                // The borrow expires after two weeks
                entity.ReturnDate = entity.BorrowDate.AddDays(14);

                // Update stocks
                foreach (var stock in entity.Stocks)
                {
                    stock.NumberOfBooksForBorrowing -= 1;
                    _ = this.stockRepository.Update(stock);
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
        public bool CheckCanBooksBeGranted(Borrow entity)
        {
            var booksToBorrow = this.GetBooksToBorrow(entity);

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
        /// <returns>bool.</returns>
        public bool CheckCanBorrowMaxNMCInPERMonths(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var per = properties.Per;
            var nmc = properties.Nmc;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                per /= 2;
                nmc *= 2;
            }

            var datePer = entity.BorrowDate.AddMonths(-per);

            // Borrows that have been done in the last PER months
            var borrows = this.Repository
                .GetBorrowsByReaderWithinDate(entity.Reader.Id, datePer);

            // Number of the books that have been borrowed in the last PER months
            var numberOfBorrowedBooks = borrows
                .SelectMany(this.GetBooksToBorrow)
                .Distinct()
                .Count();

            if (numberOfBorrowedBooks + entity.Stocks.Count > nmc)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the borrowed books for maximum C books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public bool CheckBorrowedBooksForMaxCBooks(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var c = properties.C;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
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

            var booksToBorrow = this.GetBooksToBorrow(entity);

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

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                d *= 2;
            }

            // Borrows made by reader in the last L months
            var borrows = this.Repository
                .GetBorrowsByReaderWithinDate(entity.Reader.Id, entity.BorrowDate.AddMonths(-l));

            // Books that have been borrowed by reader in the last L months
            var books = borrows
                .SelectMany(this.GetBooksToBorrow)
                .Distinct()
                .ToList();

            books.AddRange(this.GetBooksToBorrow(entity));

            Dictionary<Domain, int> domains = new Dictionary<Domain, int>();

            books.ForEach(book =>
            {
                foreach (var domain in book.Domains)
                {
                    var rootDomain = domain.GetRootDomain();
                    if (!domains.TryGetValue(rootDomain, out int value))
                    {
                        domains.Add(rootDomain, 1);
                    }
                    else
                    {
                        domains[rootDomain] = ++value;
                    }
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

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                lim *= 2;
            }

            var booksToBorrow = this.GetBooksToBorrow(entity);

            foreach (var book in booksToBorrow)
            {
                var bookBorrowCount = this.Repository
                    .GetBookBorrowCountByReaderWithinDate(book.Id, entity.Reader.Id, entity.BorrowDate.AddMonths(-3));

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
        /// <returns>Bool.</returns>
        public bool CheckBorrowsMadeInDELTADays(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var delta = properties.Delta;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                delta /= 2;
            }

            var booksToBorrow = this.GetBooksToBorrow(entity);

            foreach (var book in booksToBorrow)
            {
                var lastBorrow = this.Repository
                    .GetLastBookBorrowedByReader(book.Id, entity.Reader.Id);

                if (lastBorrow != null)
                {
                    var daysSinceLastBorrow = (entity.BorrowDate - lastBorrow.BorrowDate).TotalDays;
                    if (daysSinceLastBorrow <= delta)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckCanBorrowAtMostNCZBooksInOneDay(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var ncz = properties.Ncz;
            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                return true;
            }

            var allBorrows = this.Repository.Get(
                borrow => borrow.BorrowDate.Date == entity.BorrowDate.Date &&
                          borrow.Reader.Id == entity.Reader.Id,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return allBorrows < ncz;
        }

        /// <summary>
        /// Checks if librarian granted at most PERSIMP books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        public bool CheckGrantAtMostPERSIMPBooksInOneDay(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var persimp = properties.Persimp;

            var allBorrows = this.Repository.Get(
                borrow => borrow.BorrowDate.Date == entity.BorrowDate.Date &&
                          borrow.Librarian.Id == entity.Librarian.Id,
                borrow => borrow.OrderBy(x => x.Id)).Count();

            return allBorrows < persimp;
        }

        /// <summary>
        /// Checks borrow additional rules.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if all borrow additional rules succeed, <c>false</c> otherwise.</returns>
        public bool CheckAdditionalRules(Borrow entity)
        {
            /* O carte poate fi imprumutata daca */
            // 1. nu are toate exemplarele marcate ca fiind doar pentru sala de lectura;
            // 2. numarul de carti ramase (inca neimprumutate, dar nu din cele
            // pentru sala de lectura) este macar 10 % din fondul initial din acea carte.
            if (!this.CheckCanBooksBeGranted(entity))
            {
                return false;
            }

            // Cititorii pot imprumuta un numar maxim de carti NMC intr-o perioada PER;
            if (!this.CheckCanBorrowMaxNMCInPERMonths(entity))
            {
                return false;
            }

            // Cititorii pot prelua la un imprumut cel mult C carti; daca numarul cartilor imprumutate la o
            // cerere de imprumut e cel putin 3, atunci acestea trebui sa faca parte din cel putin 2
            // categorii distincte
            if (!this.CheckBorrowedBooksForMaxCBooks(entity))
            {
                return false;
            }

            // Cititorii nu pot imprumuta mai mult de D carti dintr-un acelasi domeniu
            // – de tip frunza sau de nivel superior - in ultimele L luni
            if (!this.CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(entity))
            {
                return false;
            }

            // Cititorii pot imprumuta o carte pe o perioada determinata; se permit prelungiri, dar suma
            // acestor prelungiri acordate in ultimele 3 luni nu poate depasi o valoare limita LIM data
            if (!this.CheckBorrowExtensionAtMostLIM(entity))
            {
                return false;
            }

            // Cititorii nu pot imprumuta aceeasi carte de mai multe ori intr-un interval DELTA specificat, unde
            // DELTA se masoara de la ultimul imprumut al cartii
            if (!this.CheckBorrowsMadeInDELTADays(entity))
            {
                return false;
            }

            // Cititorii pot imprumuta cel mult NCZ carti intr-o zi;
            // pentru personalul bibliotecii se ignora acest prag.
            if (!this.CheckCanBorrowAtMostNCZBooksInOneDay(entity))
            {
                return false;
            }

            // Personalul bibliotecii nu poate acorda mai mult de PERSIMP carti intr-o zi.
            if (!this.CheckGrantAtMostPERSIMPBooksInOneDay(entity))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the books to borrow.
        /// </summary>
        /// <param name="entity">Borrow.</param>
        /// <returns>Books.</returns>
        private List<Book> GetBooksToBorrow(Borrow entity)
        {
            var booksToBorrow = new List<Book>();

            foreach (var stock in entity.Stocks)
            {
                var book = this.bookRepository.GetBookByStockId(stock.Id);
                if (book != null)
                {
                    booksToBorrow.Add(book);
                }
            }

            return booksToBorrow;
        }
    }
}
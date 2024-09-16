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
        /// <returns>Bool.</returns>
        public override bool Insert(Borrow entity)
        {
            var result = this.Validator.Validate(entity);
            if (!result.IsValid)
            {
                Logging.LogErrors(result);
                return false;
            }

            if (!this.CheckAdditionalRules(entity))
            {
                Logging.LogErrors($"Additional rules for {entity} were not met!");
                return false;
            }

            // The borrow expires after two weeks
            entity.ReturnDate = entity.BorrowDate.AddDays(14);

            _ = this.Repository.Insert(entity);

            // Update stocks
            foreach (var stock in entity.Stocks)
            {
                stock.NumberOfBooksForBorrowing -= 1;
                _ = this.stockRepository.Update(stock);
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CheckCanBooksBeGranted(Borrow entity)
        {
            var booksToBorrow = this.GetBooksToBorrow(entity);

            foreach (var book in booksToBorrow)
            {
                var stocks = this.stockRepository.GetByBookId(book.Id);

                var initialNumberOfBooksForBorrowing = 0;
                var currentNumberOfBooksForBorrowing = 0;
                var numberOfBooksForLectureOnly = 0;

                foreach (var stock in stocks)
                {
                    initialNumberOfBooksForBorrowing +=
                        stock.InitialStock - stock.NumberOfBooksForLectureOnly;

                    currentNumberOfBooksForBorrowing +=
                        stock.NumberOfBooksForBorrowing;

                    numberOfBooksForLectureOnly +=
                        stock.NumberOfBooksForLectureOnly;
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
                if (currentNumberOfBooksForBorrowing <
                    0.1f * initialNumberOfBooksForBorrowing)
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CheckCanBorrowMaxNMCInPERMonths(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();
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
                .GetBorrowsByReaderWithinDate(
                    entity.Reader.Id,
                    datePer);

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

        /// <inheritdoc/>
        public bool CheckBorrowedBooksForMaxCBooks(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();
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

        /// <inheritdoc/>
        public bool CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();
            var d = properties.D;
            var l = properties.L;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                d *= 2;
            }

            // Borrows made by reader in the last L months
            var borrows = this.Repository
                .GetBorrowsByReaderWithinDate(
                    entity.Reader.Id,
                    entity.BorrowDate.AddMonths(-l));

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

        /// <inheritdoc/>
        public bool CheckBorrowExtensionAtMostLIM(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();
            var lim = properties.Lim;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                lim *= 2;
            }

            var booksToBorrow = this.GetBooksToBorrow(entity);

            foreach (var book in booksToBorrow)
            {
                var bookBorrowCount = this.Repository
                    .GetBorrowCountOfBookByReaderWithinDate(
                        book.Id,
                        entity.Reader.Id,
                        entity.BorrowDate.AddMonths(-3));

                if (bookBorrowCount + 1 > lim)
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CheckBorrowsMadeInDELTADays(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();
            var delta = properties.Delta;

            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                delta /= 2;
            }

            var booksToBorrow = this.GetBooksToBorrow(entity);

            foreach (var book in booksToBorrow)
            {
                var lastBorrow = this.Repository
                    .GetLastBorrowOfBookByReader(book.Id, entity.Reader.Id);

                if (lastBorrow != null)
                {
                    var daysSinceLastBorrow =
                        (entity.BorrowDate - lastBorrow.BorrowDate).TotalDays;

                    if (daysSinceLastBorrow <= delta)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CheckCanBorrowAtMostNCZBooksInOneDay(Borrow entity)
        {
            if (entity.Reader.UserType == EUserType.LibrarianReader)
            {
                return true;
            }

            var properties = this.PropertiesRepository.GetLast();
            var ncz = properties.Ncz;

            var allBorrows = this.Repository.Get(
                borrow => borrow.BorrowDate.Date == entity.BorrowDate.Date &&
                          borrow.Reader.Id == entity.Reader.Id,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return allBorrows < ncz;
        }

        /// <inheritdoc/>
        public bool CheckGrantAtMostPERSIMPBooksInOneDay(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLast();

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
        private bool CheckAdditionalRules(Borrow entity)
        {
            if (!this.CheckCanBooksBeGranted(entity))
            {
                return false;
            }

            if (!this.CheckCanBorrowMaxNMCInPERMonths(entity))
            {
                return false;
            }

            if (!this.CheckBorrowedBooksForMaxCBooks(entity))
            {
                return false;
            }

            if (!this.CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(entity))
            {
                return false;
            }

            if (!this.CheckBorrowExtensionAtMostLIM(entity))
            {
                return false;
            }

            if (!this.CheckBorrowsMadeInDELTADays(entity))
            {
                return false;
            }

            if (!this.CheckCanBorrowAtMostNCZBooksInOneDay(entity))
            {
                return false;
            }

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
                var book = this.bookRepository
                    .GetByStockId(stock.Id);

                if (book != null)
                {
                    booksToBorrow.Add(book);
                }
            }

            return booksToBorrow;
        }
    }
}
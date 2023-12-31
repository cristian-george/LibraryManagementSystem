﻿// <copyright file="BorrowService.cs" company="Transilvania University of Brasov">
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
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Borrow, IBorrowRepository}" />
    /// Implements the <see cref="IBorrowService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Borrow, IBorrowRepository}" />
    /// <seealso cref="IBorrowService" />
    public class BorrowService : BaseService<Borrow, IBorrowRepository, IPropertiesRepository>, IBorrowService
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
        /// <param name="entity"> The entity. </param>
        /// <returns> ceva. </returns>
        public override bool Insert(Borrow entity)
        {
            // un imprumut are maxim 1 luna
            if (entity.BorrowDate != null)
            {
                var startDate = (DateTime)entity.BorrowDate;
                entity.EndDate = startDate.AddMonths(1);
            }

            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckFlags(entity))
            {
                _ = this.Repository.Insert(entity);
            }
            else
            {
                _ = LogUtils.LogErrors(result);
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

            var c = this.PropertiesRepository.GetLastProperties().C;

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
        /// Checks the borrow in delta time.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckBorrowInDELTATime(Borrow entity)
        {
            if (entity.BorrowedBooks.Count != 1)
            {
                return false;
            }

            var properties = this.PropertiesRepository.GetLastProperties();
            var delta = this.PropertiesRepository.GetLastProperties().DELTA;

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
        /// Checks the can borrow maximum NMC in PER.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckCanBorrowMaxNMCInPER(Borrow entity)
        {
            var properties = this.PropertiesRepository.GetLastProperties();
            var per = this.PropertiesRepository.GetLastProperties().PER;
            var nmc = this.PropertiesRepository.GetLastProperties().NMC;

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
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckMaxBorrowBooksToday()
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            var borrowsToday = this.Repository.Get(
                borrow => borrow.BorrowDate == DateTime.Today,
                borrow => borrow.OrderBy(x => x.Id),
                string.Empty).Count();

            return borrowsToday == 0 || properties.NCZ < borrowsToday;
        }

        /// <summary>
        /// Checks if books are borrowable.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckIfBooksAreBorrowable(Borrow entity)
        {
            if (entity.BorrowedBooks.Count == 1)
            {
                var book = entity.BorrowedBooks.First();

                if (book.LecturesOnlyBook == true || this.CanBorrowBook(book.Title) || this.CheckLIM(entity))
                {
                    return false;
                }
            }
            else
            {
                foreach (var book in entity.BorrowedBooks)
                {
                    if (book.LecturesOnlyBook == true || this.CanBorrowBook(book.Title))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the lim.
        /// </summary>
        /// <param name="entity"> entity.</param>
        /// <returns> vrbs. </returns>
        public bool CheckLIM(Borrow entity)
        {
            // Partea cu ultimele 3 luni trebuie facuta cand se face update si se doreste sa se faca extindere
            var properties = this.PropertiesRepository.GetLastProperties();

            if (entity.NoOfTimeExtended >= properties.LIM)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance [can borrow book] the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns><c>true</c> if this instance [can borrow book] the specified title; otherwise, <c>false</c>.</returns>
        public bool CanBorrowBook(string title)
        {
            var allBooksWithTheSameName = this.bookRepository.GetBooksWithTheSameTitle(title);
            var unavailableBooks = this.bookRepository.GetUnavailableBooks(allBooksWithTheSameName);

            var allBooksWithTheSameNameCount = allBooksWithTheSameName.Count();
            var unavailableBooksCount = unavailableBooks.Count();

            return allBooksWithTheSameNameCount * 0.1f >=
                   allBooksWithTheSameNameCount - unavailableBooksCount;
        }

        /// <summary>
        /// Checks the flags.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckFlags(Borrow entity)
        {
            if (!this.Repository.Get(null, borrow => borrow.OrderBy(x => x.Id), string.Empty).Any())
            {
                return true;
            }

            // 1. Daca cartea este doar pentru sala de lectura
            // 2. O carte poate fi imprumutata doar daca: nu are toate exemplarele marcate ca fiind doar
            // pentru sala de lectura si numarul de carti ramase(inca neimprumutate, dar nu din cele
            // pentru sala de lectura) este macar 10 % din fondul initial din acea carte.
            // 3. Pot imprumuta o carte pe o perioada determinata; se permit prelungiri, dar suma
            // acestor prelungiri acordate in ultimele 3 luni nu poate depasi o valoare limita LIM data
            // Un imprumut are 2 saptamani maxim
            if (this.CheckIfBooksAreBorrowable(entity) == false)
            {
                return false;
            }

            // 1. Pot imprumuta un numar maxim de carti NMC intr-o perioada PER;
            if (this.CheckCanBorrowMaxNMCInPER(entity) == false)
            {
                return false;
            }

            // 2. La un imprumut pot prelua cel mult C carti; daca numarul cartilor imprumutate la o
            // cerere de imprumut e cel putin 3, atunci acestea trebui sa faca parte din cel putin 2
            // categorii distincte
            if (this.CheckBorrowedBooksForMaxCBooks(entity) == false)
            {
                return false;
            }

            // 5. Nu pot imprumuta aceeasi carte de mai multe ori intr-un interval DELTA specificat, unde
            // DELTA se masoara de la ultimul imprumut al cartii
            if (this.CheckBorrowInDELTATime(entity) == false)
            {
                return false;
            }

            // 6. Pot imprumuta cel mult NCZ carti intr-o zi.
            if (this.CheckMaxBorrowBooksToday() == false)
            {
                return false;
            }

            // Nu pot imprumuta mai mult de D carti dintr-un acelasi domeniu – de tip frunza sau de
            // nivel superior - in ultimele L luni
            // crapa metoda si n am mai avut timp de testat
            foreach (var book in entity.BorrowedBooks)
            {
                book.IsBorrowed = true;
            }

            return true;
        }
    }
}
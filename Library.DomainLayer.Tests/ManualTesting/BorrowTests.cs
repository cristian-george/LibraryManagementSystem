// <copyright file="BorrowTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BorrowTests.
    /// </summary>
    [TestClass]
    public class BorrowTests
    {
        /// <summary>
        /// The borrow.
        /// </summary>
        private Borrow borrow;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.borrow = new Borrow();
        }

        /// <summary>
        /// Defines the test method ReaderShouldBeValid.
        /// </summary>
        [TestMethod]
        public void ReaderShouldBeValid()
        {
            var reader = new User
            {
                FirstName = "Soarece",
                LastName = "de Biblitoeca",
                Address = "Street Doua Lebede, nr. 80",
                Email = "soarece@debiblitoeca.ro",
                PhoneNumber = "0724525672",
                UserType = Enums.EUserType.Reader,
            };

            this.borrow.Reader = reader;

            Assert.IsNotNull(this.borrow.Reader);
        }

        /// <summary>
        /// Defines the test method ReaderShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LibrarianShouldBeValid()
        {
            var librarian = new User
            {
                FirstName = "Bibliotecar",
                LastName = "Secret",
                Address = "Street George Baritiu, nr. 10",
                Email = "bibliotecar@secret.ro",
                PhoneNumber = "0724525672",
                UserType = Enums.EUserType.Librarian,
            };

            this.borrow.Librarian = librarian;

            Assert.IsNotNull(this.borrow.Librarian);
        }

        /// <summary>
        /// Defines the test method StocksShouldBeValid.
        /// </summary>
        [TestMethod]
        public void StocksShouldBeValid()
        {
            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
            };

            var domain = new Domain()
            {
                Name = "Informatica",
                ParentDomain = null,
                ChildrenDomains = null,
            };

            Book book = new Book()
            {
                Title = "O carte de citit",
                Genre = "Necunoscut",
                Authors = new List<Author> { author },
                Domains = new List<Domain> { domain },
            };

            var edition = new Edition()
            {
                Publisher = "Casa de carti",
                Year = 1987,
                EditionNumber = 1,
                NumberOfPages = 200,
                Book = book,
            };

            book.Editions = new List<Edition>() { edition };

            var stock = new Stock()
            {
                SupplyDate = DateTime.Today.AddDays(-5),
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                InitialStock = 20,
                Edition = edition,
            };

            this.borrow.Stocks = new List<Stock>() { stock };

            Assert.IsNotNull(this.borrow.Stocks);
            Assert.IsTrue(this.borrow.Stocks.Count != 0);

            var s = this.borrow.Stocks.FirstOrDefault();
            Assert.IsNotNull(s);
            Assert.IsTrue(s.NumberOfBooksForLectureOnly > 0);
            Assert.IsTrue(s.NumberOfBooksForBorrowing > 0);

            var e = s.Edition;
            Assert.IsNotNull(e);
            Assert.IsTrue(e.Year >= 1850 && e.Year <= 2024);

            var b = e.Book;
            Assert.IsNotNull(b);
            Assert.IsTrue(b.Authors.Count != 0);
        }

        /// <summary>
        /// Defines the test method BorrowDateShouldNotBeHigherThanCurrentDate.
        /// </summary>
        [TestMethod]
        public void BorrowDateShouldNotBeHigherThanCurrentDate()
        {
            this.borrow.BorrowDate = DateTime.Now.AddHours(1);

            var flag = this.borrow.BorrowDate > DateTime.Now;
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method EndDateShouldNotExceed14Days.
        /// </summary>
        [TestMethod]
        public void EndDateShouldNotExceed14Days()
        {
            this.borrow.ReturnDate = DateTime.Now.AddDays(10);

            var flag = this.borrow.ReturnDate > DateTime.Now.AddDays(14);
            Assert.IsFalse(flag);
        }
    }
}
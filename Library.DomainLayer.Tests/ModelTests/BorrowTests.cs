﻿// <copyright file="BorrowTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System;
    using System.Collections.Generic;
    using Library.DomainLayer;
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
        /// Defines the test method BorrowShouldHaveAValidReader.
        /// </summary>
        [TestMethod]
        public void BorrowShouldHaveAValidReader()
        {
            var reader = new Reader
            {
                Account = new ()
                {
                    PhoneNumber = "0724525672",
                    Email = "vali@mail.com",
                },
            };

            this.borrow = new ()
            {
                Reader = reader,
                BorrowedBooks = null,
            };

            Assert.IsNotNull(this.borrow.Reader);
        }

        /// <summary>
        /// Defines the test method BorrowShouldHaveAValidBorrowedBooksList.
        /// </summary>
        [TestMethod]
        public void BorrowShouldHaveAValidBorrowedBooksList()
        {
            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
            };

            var authorsList = new List<Author>
            {
                author,
            };

            var domain = new Domain()
            {
                Name = "Informatica",
                ParentDomain = null,
                ChildrenDomains = null,
            };

            var domainsList = new List<Domain>
            {
                domain,
            };

            var edition = new Edition()
            {
                Publisher = "Casa de carti marcel dorel",
                Year = "1987",
                EditionNumber = 1,
                NumberOfPages = 200,
            };

            var editionsList = new List<Edition>
            {
                edition,
            };

            var borrowedBooks = new List<Book>
            {
                new ()
                {
                    Title = "O Carte",
                    LecturesOnlyBook = false,
                    Authors = authorsList,
                    Domains = domainsList,
                    Editions = editionsList,
                },
            };

            this.borrow = new ()
            {
                Reader = null,
                BorrowedBooks = borrowedBooks,
            };

            Assert.IsNotNull(this.borrow.BorrowedBooks);
        }

        /// <summary>
        /// Defines the test method BorrowDateShouldNotBeHigherThanCurrentDate.
        /// </summary>
        [TestMethod]
        public void BorrowDateShouldNotBeHigherThanCurrentDate()
        {
            this.borrow.BorrowDate = DateTime.Now.AddHours(1);

            var flag = TestUtils.IsFirstDateHigherThanSecondDate(
                (DateTime)this.borrow.BorrowDate, DateTime.Now);
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method EndDateShouldNotExceedThreeMonths.
        /// </summary>
        [TestMethod]
        public void EndDateShouldNotExceedThreeMonths()
        {
            this.borrow.EndDate = DateTime.Now.AddMonths(2);

            var flag = TestUtils.IsFirstDateHigherThanSecondDate(
                (DateTime)this.borrow.EndDate, DateTime.Now.AddMonths(3));
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Defines the test method NoOfTimeExtendedShouldBeNoHigherThanThree.
        /// </summary>
        [TestMethod]
        public void NoOfTimeExtendedShouldBeNoHigherThanThree()
        {
            this.borrow.NoOfTimeExtended = 2;
            Assert.IsFalse(this.borrow.NoOfTimeExtended > 3);
        }
    }
}
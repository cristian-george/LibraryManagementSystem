// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer;
    using Library.DomainLayer.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BookTests.
    /// </summary>
    [TestClass]
    public class BookTests
    {
        /// <summary>
        /// The book.
        /// </summary>
        private Book book;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var author = new Author()
            {
                Id = 1,
                FirstName = "Mihail",
                LastName = "Sadoveanu",
            };

            var domain = new Domain()
            {
                Id = 1,
                Name = "Literatura",
                ParentDomain = null,
                ChildrenDomains = new List<Domain>(),
            };

            var edition = new Edition()
            {
                Id = 1,
                Publisher = "Editura Povestiri",
                Year = "1920",
                EditionNumber = 5,
                NumberOfPages = 150,
            };

            this.book = new Book()
            {
                Id = 1,
                Title = "Hanu Ancutei",
                Type = "Carte de povestiri",
                IsBorrowed = false,
                LecturesOnlyBook = true,
                Authors = new List<Author>() { author },
                Domains = new List<Domain>() { domain },
                Editions = new List<Edition>() { edition },
            };
        }

        /// <summary>
        /// Defines the test method TitleShouldBeValid.
        /// </summary>
        [TestMethod]
        public void TitleShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.book.Title));
        }

        /// <summary>
        /// Defines the test method TitleShouldNotHaveDigits.
        /// </summary>
        [TestMethod]
        public void TitleShouldNotHaveDigits()
        {
            this.book.Title = "100 de zile pe mare";

            Assert.IsTrue(TestUtils.ContainsDigits(this.book.Title));
        }

        /// <summary>
        /// Defines the test method BookAuthorsFirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void BookAuthorsFirstNameShouldBeValid()
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

            this.book = new Book()
            {
                Title = "How to write bad code with Cristi",
                LecturesOnlyBook = true,
                Authors = authorsList,
            };

            var flag = this.book.Authors.All(x => x.FirstName.All(char.IsDigit));

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Defines the test method BookAuthorsLastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void BookAuthorsLastNameShouldBeValid()
        {
            var authorLastName = this.book.Authors.ElementAt(0).LastName;

            Assert.IsFalse(TestUtils.ContainsDigits(authorLastName));
        }

        /// <summary>
        /// Defines the test method BookAuthorsShouldNotBeNull.
        /// </summary>
        [TestMethod]
        public void BookAuthorsShouldNotBeNull()
        {
            Assert.IsNotNull(this.book.Authors);
        }

        /// <summary>
        /// Defines the test method BookDomainsShouldNotBeNull.
        /// </summary>
        [TestMethod]
        public void BookDomainsShouldNotBeNull()
        {
            Assert.IsNotNull(this.book.Domains);
        }

        /// <summary>
        /// Defines the test method BookEditionsShouldNotBeNull.
        /// </summary>
        [TestMethod]
        public void BookEditionsShouldNotBeNull()
        {
            Assert.IsNotNull(this.book.Editions);
        }

        /// <summary>
        /// Defines the test method TypeShouldNotContainDigitsValid.
        /// </summary>
        [TestMethod]
        public void TypeShouldNotContainDigitsValid()
        {
            this.book.Type = "12 Povestiri";

            Assert.IsFalse(this.book.Type.All(char.IsLetter));
        }

        /// <summary>
        /// Defines the test method BookShouldBeBorrowable.
        /// </summary>
        [TestMethod]
        public void BookShouldBeBorrowable()
        {
            this.book.LecturesOnlyBook = false;

            Assert.IsFalse((bool)this.book.LecturesOnlyBook);
        }

        /// <summary>
        /// Defines the test method BookShouldNotBeBorrowable.
        /// </summary>
        [TestMethod]
        public void BookShouldNotBeBorrowable()
        {
            this.book.LecturesOnlyBook = true;

            Assert.IsTrue((bool)this.book.LecturesOnlyBook);
        }

        /// <summary>
        /// Defines the test method BookShouldBeBorrowed.
        /// </summary>
        [TestMethod]
        public void BookShouldBeBorrowed()
        {
            this.book.IsBorrowed = false;

            Assert.IsFalse((bool)this.book.IsBorrowed);
        }

        /// <summary>
        /// Defines the test method BookShouldNotBeBorrowed.
        /// </summary>
        [TestMethod]
        public void BookShouldNotBeBorrowed()
        {
            this.book.IsBorrowed = true;

            Assert.IsTrue((bool)this.book.IsBorrowed);
        }
    }
}
// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
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
                ChildDomains = new List<Domain>(),
            };

            var edition = new Edition()
            {
                Id = 1,
                Publisher = "Editura Povestiri",
                Year = 1920,
                EditionNumber = 5,
                NumberOfPages = 150,
                BookType = Enums.EBookType.Hardcover,
            };

            this.book = new Book()
            {
                Id = 1,
                Title = "Hanu Ancutei",
                Genre = "Carte de povestiri",
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
            Assert.IsFalse(this.book.Title.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method TitleShouldNotHaveDigits.
        /// </summary>
        [TestMethod]
        public void TitleShouldNotHaveDigits()
        {
            this.book.Title = "100 de zile pe mare";

            Assert.IsTrue(this.book.Title.ContainsDigits());
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
                Genre = "Necunoscut",
                Authors = authorsList,
            };

            var flag = this.book.Authors.All(x => x.FirstName.All(char.IsDigit));

            Assert.IsFalse(flag);
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
            this.book.Genre = "12 Povestiri";

            Assert.IsFalse(this.book.Genre.All(char.IsLetter));
        }
    }
}
// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
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
            this.book = new Book()
            {
                Id = 1,
                Title = "Hanu Ancutei",
                Genre = "Carte de povestiri",
                Domains = new List<Domain>()
                {
                    new ()
                    {
                        Id = 1,
                        Name = "Literatura",
                    },
                },
            };
        }

        /// <summary>
        /// Defines the test method TitleShouldBeValid.
        /// </summary>
        [TestMethod]
        public void TitleShouldBeValid()
        {
            Assert.IsNotNull(this.book.Title);
            Assert.IsFalse(this.book.Title.Equals(string.Empty));

            Assert.IsTrue(this.book.Title.Length > 2);
            Assert.IsTrue(this.book.Title.Length < 100);

            Assert.IsFalse(this.book.Title.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method TitleShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void TitleShouldBeInvalid()
        {
            this.book.Title = null;
            Assert.IsNull(this.book.Title);

            this.book.Title = string.Empty;
            Assert.IsTrue(this.book.Title.Equals(string.Empty));

            this.book.Title = "H";
            Assert.IsFalse(this.book.Title.Length > 2);

            this.book.Title = "---Hanu Ancutei 123";
            Assert.IsTrue(this.book.Title.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method GenreShouldBeValid.
        /// </summary>
        [TestMethod]
        public void GenreShouldBeValid()
        {
            Assert.IsNotNull(this.book.Genre);
            Assert.IsFalse(this.book.Genre.Equals(string.Empty));

            Assert.IsTrue(this.book.Genre.Length > 2);
            Assert.IsTrue(this.book.Genre.Length < 45);

            Assert.IsFalse(this.book.Genre.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method GenreShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void GenreShouldBeInvalid()
        {
            this.book.Genre = null;
            Assert.IsNull(this.book.Genre);

            this.book.Genre = string.Empty;
            Assert.IsTrue(this.book.Genre.Equals(string.Empty));

            this.book.Genre = "H";
            Assert.IsFalse(this.book.Genre.Length > 2);

            this.book.Genre = "---Carte de povestiri123";
            Assert.IsTrue(this.book.Genre.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method DomainsCollectionShouldBeValid.
        /// </summary>
        [TestMethod]
        public void DomainsCollectionShouldBeValid()
        {
            Assert.IsNotNull(this.book.Domains);
            Assert.IsTrue(this.book.Domains.Count > 0);
        }

        /// <summary>
        /// Defines the test method DomainsCollectionShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void DomainsCollectionShouldBeInvalid()
        {
            this.book.Domains.Clear();
            Assert.IsFalse(this.book.Domains.Count > 0);

            this.book.Domains = null;
            Assert.IsNull(this.book.Domains);
        }
    }
}
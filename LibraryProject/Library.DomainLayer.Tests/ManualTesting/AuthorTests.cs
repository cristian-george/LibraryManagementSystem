// <copyright file="AuthorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Collections.Generic;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AuthorTests.
    /// </summary>
    [TestClass]
    public class AuthorTests
    {
        /// <summary>
        /// The entity.
        /// </summary>
        private Author author;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.author = new Author()
            {
                Id = 1,
                FirstName = "Mihail",
                LastName = "Sadoveanu",
                Books = new List<Book>()
                {
                    new ()
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
                    },
                },
            };
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(this.author.FirstName == null);
            Assert.IsFalse(this.author.FirstName == string.Empty);

            Assert.IsTrue(this.author.FirstName.Length >= 2);
            Assert.IsTrue(this.author.FirstName.Length <= 50);

            Assert.IsFalse(this.author.FirstName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.author.FirstName = null;
            Assert.IsTrue(this.author.FirstName == null);

            this.author.FirstName = string.Empty;
            Assert.IsTrue(this.author.FirstName == string.Empty);

            this.author.FirstName = "M";
            Assert.IsFalse(this.author.FirstName.Length >= 2);

            this.author.FirstName = new string('M', 51);
            Assert.IsFalse(this.author.FirstName.Length <= 50);

            this.author.FirstName = "Mih12";
            Assert.IsTrue(this.author.FirstName.CountDigits() != 0);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            Assert.IsFalse(this.author.LastName == null);
            Assert.IsFalse(this.author.LastName == string.Empty);

            Assert.IsTrue(this.author.LastName.Length >= 2);
            Assert.IsTrue(this.author.LastName.Length <= 50);

            Assert.IsFalse(this.author.LastName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.author.LastName = null;
            Assert.IsTrue(this.author.LastName == null);

            this.author.LastName = string.Empty;
            Assert.IsTrue(this.author.LastName == string.Empty);

            this.author.LastName = "S";
            Assert.IsFalse(this.author.LastName.Length >= 2);

            this.author.LastName = new string('S', 51);
            Assert.IsFalse(this.author.LastName.Length <= 50);

            this.author.LastName = "Sadov12";
            Assert.IsTrue(this.author.LastName.CountDigits() != 0);
        }

        /// <summary>
        /// Defines the test method BooksCollectionShouldBeValid.
        /// </summary>
        [TestMethod]
        public void BooksCollectionShouldBeValid()
        {
            Assert.IsNotNull(this.author.Books);
            Assert.IsTrue(this.author.Books.Count > 0);
        }

        /// <summary>
        /// Defines the test method BooksCollectionShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void BooksCollectionShouldBeInvalid()
        {
            this.author.Books.Clear();
            Assert.IsFalse(this.author.Books.Count > 0);

            this.author.Books = null;
            Assert.IsNull(this.author.Books);
        }
    }
}
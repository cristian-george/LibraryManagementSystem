// <copyright file="EditionServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class EditionServiceTests.
    /// </summary>
    [TestClass]
    public class EditionServiceTests
    {
        private EditionService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<EditionService>();
        }

        /// <summary>
        /// Defines the test method EndToEndEdition.
        /// </summary>
        [TestMethod]
        public void EndToEndEdition()
        {
            var domain = new Domain()
            {
                Name = "Literatura",
            };

            var author = new Author()
            {
                FirstName = "Test",
                LastName = "Test",
                Books = new List<Book>(),
            };

            var book = new Book()
            {
                Title = "Arhitectura calculatoarelor",
                Genre = "Literatura",
                Authors = new List<Author>(),
                Domains = new List<Domain>() { domain },
            };

            author.Books.Add(book);
            book.Authors.Add(author);

            var edition = new Edition()
            {
                Book = book,
                BookType = DomainLayer.Enums.EBookType.Hardcover,
                Publisher = "Editura Cartea",
                Year = 1999,
                EditionNumber = 8,
                NumberOfPages = 50,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(edition));

            // GetAll
            var allEditions = this.service.Get();
            Assert.IsNotNull(allEditions);

            // GetById
            var id = allEditions.LastOrDefault().Id;
            var dbEdition = this.service.GetById(id);
            Assert.IsNotNull(dbEdition);
            Assert.IsTrue(dbEdition.BookType == EBookType.Hardcover);

            Assert.IsTrue(book.Editions.Count > 0);
            Assert.IsTrue(book.Editions.Contains(dbEdition));

            // Update
            edition.Year = 2005;
            Assert.IsTrue(this.service.Update(dbEdition));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbEdition.Id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());
        }
    }
}

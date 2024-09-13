// <copyright file="BookServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BookServiceTests.
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        private BookService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<BookService>();

            // Add properties
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                Domenii = 2,
                Nmc = 3,
                L = 2,
                C = 3,
                D = 2,
                Lim = 2,
                Delta = 3,
                Ncz = 4,
                Persimp = 3,
                Per = 3,
            };

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));
        }

        /// <summary>
        /// Defines the test method EndToEndBook.
        /// </summary>
        [TestMethod]
        public void EndToEndBook()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
            };

            var subdomain = new Domain()
            {
                Name = "Chimie",
                ParentDomain = domain,
            };

            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
                Books = new List<Book>(),
            };

            var book = new Book()
            {
                Title = "Head first design patters",
                Genre = "Programming",
                Domains = new List<Domain>() { subdomain },
                Authors = new List<Author>(),
            };

            author.Books.Add(book);
            book.Authors.Add(author);

            // Insert
            Assert.IsTrue(this.service.Insert(book));

            // GetAll
            var allBooks = this.service.Get();
            Assert.IsNotNull(allBooks);

            // GetById
            var id = allBooks.LastOrDefault().Id;
            var dbBook = this.service.GetById(id);
            Assert.IsNotNull(dbBook);

            // Update
            dbBook.Title = "Smart things in programming";
            Assert.IsTrue(this.service.Update(dbBook));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbBook.Id));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void IsInParentChildRelationDomainsShouldReturnTrue()
        {
            var rootDomain = new Domain()
            {
                Name = "Stiinta",
            };

            var subdomain = new Domain()
            {
                Name = "Chimie",
                ParentDomain = rootDomain,
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { rootDomain, subdomain },
            };

            Assert.IsTrue(this.service.IsInParentChildRelationDomains(book));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void IsInParentChildRelationDomainsShouldReturnFalse()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
            };

            var otherDomain = new Domain()
            {
                Name = "Literatura",
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { domain, otherDomain },
            };

            Assert.IsFalse(this.service.IsInParentChildRelationDomains(book));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void IsInMoreThanNDomainsShouldReturnTrue()
        {
            var domain1 = new Domain()
            {
                Name = "Stiinta1",
            };

            var domain2 = new Domain()
            {
                Name = "Stiinta2",
            };

            var domain3 = new Domain()
            {
                Name = "Stiint3",
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { domain1, domain2, domain3 },
            };

            Assert.IsTrue(this.service.IsInMoreThanNDomains(book));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void IsInMoreThanNDomainsShouldReturnFalse()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
            };

            var otherDomain = new Domain()
            {
                Name = "Literatura",
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { domain, otherDomain },
            };

            Assert.IsFalse(this.service.IsInMoreThanNDomains(book));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean Book table
            _ = this.service.Delete();

            // Clean Properties table
            var propertiesService = Injector.Create<PropertiesService>();
            Assert.IsTrue(propertiesService.Delete());

            // Clean Author table
            var authorService = Injector.Create<AuthorService>();
            Assert.IsTrue(authorService.Delete());

            // Clean Domain table
            var domainService = Injector.Create<DomainService>();
            Assert.IsTrue(domainService.Delete());
        }
    }
}

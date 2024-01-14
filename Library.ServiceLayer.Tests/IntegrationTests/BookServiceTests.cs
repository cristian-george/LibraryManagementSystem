﻿// <copyright file="BookServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer;
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
        }

        /// <summary>
        /// Defines the test method EndToEndBook.
        /// </summary>
        [TestMethod]
        public void EndToEndBook()
        {
            // Add properties
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                DOMENII = 2,
                NMC = 3,
                L = 2,
                C = 3,
                D = 2,
                LIM = 2,
                DELTA = 3,
                NCZ = 4,
                PERSIMP = 3,
                PER = 3,
            };

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
            };

            var domain = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
                ChildrenDomains = new List<Domain>(),
            };

            var edition = new Edition()
            {
                Publisher = "Cartea studentilor saraci",
                Year = "1999",
                EditionNumber = int.MaxValue,
                NumberOfPages = 25,
            };

            var book = new Book()
            {
                Title = "Head first design patters",
                LecturesOnlyBook = false,
                IsBorrowed = false,
                Type = "Hard  cover",
                Authors = new List<Author>() { author },
                Domains = new List<Domain>() { domain },
                Editions = new List<Edition>() { edition },
            };

            // Insert
            Assert.IsTrue(this.service.Insert(book));

            // GetAll
            var allBooks = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allBooks);

            // GetById
            var id = allBooks.LastOrDefault().Id;
            var dbBook = this.service.GetById(id);
            Assert.IsNotNull(dbBook);

            // Update
            dbBook.Title = "Idiot things in programming";
            Assert.IsTrue(this.service.Update(dbBook));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbBook.Id));
        }

        /// <summary>
        /// Defines the test method TestBadInsertWithBookWithNoAuthors.
        /// </summary>
        [TestMethod]
        public void TestBadInsertWithBookWithNoAuthors()
        {
            var book = new Book()
            {
                Title = string.Empty,
                LecturesOnlyBook = false,
                IsBorrowed = false,
                Type = "Hard  cover",
                Authors = new List<Author>(),
                Domains = null,
                Editions = null,
            };

            // Insert
            Assert.IsFalse(this.service.Insert(book));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void BookHasCorrectDomainsShouldReturnFalse()
        {
            var parent = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
            };

            var children = new Domain()
            {
                Name = "Chimie",
                ParentDomain = parent,
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { parent, children },
            };

            Assert.IsFalse(this.service.IsInTheCorrectDomains(book));
        }

        /// <summary>
        /// Defines the test method BookHasCorrectDomainsShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void BookHasCorrectDomainsShouldReturnTrue()
        {
            var parent = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
            };

            var parent2 = new Domain()
            {
                Name = "Literatura",
                ParentDomain = null,
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { parent, parent2 },
            };

            Assert.IsTrue(this.service.IsInTheCorrectDomains(book));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean Book table
            _ = this.service.Delete();

            // Clean Author table
            var authorService = Injector.Create<AuthorService>();
            Assert.IsTrue(authorService.Delete());

            // Clean Domain table
            var domainService = Injector.Create<DomainService>();
            Assert.IsTrue(domainService.Delete());

            // Clean Edition table
            var editionService = Injector.Create<EditionService>();
            Assert.IsTrue(editionService.Delete());

            // Clean Properties table
            var propertiesService = Injector.Create<PropertiesService>();
            Assert.IsTrue(propertiesService.Delete());
        }
    }
}

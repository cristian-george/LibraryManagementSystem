// <copyright file="StockServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class StockServiceTests.
    /// </summary>
    [TestClass]
    public class StockServiceTests
    {
        private StockService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = new StockService();

            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();
            Assert.IsTrue(propertiesService.Insert(properties));
        }

        /// <summary>
        /// Defines the test method EndToEndStock.
        /// </summary>
        [TestMethod]
        public void EndToEndStock()
        {
            var author = new Author()
            {
                FirstName = "Lucian",
                LastName = "Sasu",
                Books = new List<Book>(),
            };

            var rootDomain = ProduceModel.GetScienceDomainModel();
            var domain = rootDomain.ChildDomains.ElementAt(3) // Informatica
                                   .ChildDomains.ElementAt(2); // Baze de date

            var book = new Book()
            {
                Title = "Introducere in SQL",
                Genre = "Programare si baze de date",
                Domains = new List<Domain>() { domain },
                Authors = new List<Author>(),
            };

            author.Books.Add(book);
            book.Authors.Add(author);

            var edition = new Edition()
            {
                Book = book,
                Publisher = "Editura Universitatii",
                Year = 2015,
                EditionNumber = 15,
                NumberOfPages = 240,
                BookType = EBookType.Hardcover,
            };

            var otherEdition = new Edition()
            {
                Book = book,
                Publisher = "Editura UniTBv",
                Year = 2018,
                EditionNumber = 18,
                NumberOfPages = 270,
                BookType = EBookType.Paperback,
            };

            var reader = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "Brasov, strada Iuliu Maniu, nr. 50",
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
                UserType = EUserType.Reader,
            };

            var librarian = new User()
            {
                FirstName = "Biblioteca",
                LastName = "Judeteana",
                Address = "Brasov, Livada Postei, nr. 30",
                PhoneNumber = "0770400404",
                Email = "biblioteca_judeteana@brasov.ro",
                UserType = EUserType.Librarian,
            };

            var stock = new Stock()
            {
                Edition = edition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                SupplyDate = DateTime.Now.AddDays(-7),
            };

            // Insert
            Assert.IsTrue(this.service.Insert(stock));

            // GetAll
            var allStocks = this.service.Get();
            Assert.IsNotNull(allStocks);

            // GetById
            var id = allStocks.LastOrDefault().Id;
            var dbStock = this.service.GetById(id);
            Assert.IsNotNull(dbStock);

            // Update
            stock.SupplyDate = stock.SupplyDate.AddDays(-5);
            Assert.IsTrue(this.service.Update(dbStock));

            // Delete
            Assert.IsTrue(this.service.DeleteById(id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());

            // Clean Edition table
            var editionService = Injector.Create<EditionService>();
            Assert.IsTrue(editionService.Delete());

            // Clean Book table
            var bookService = Injector.Create<BookService>();
            Assert.IsTrue(bookService.Delete());

            // Clean Author table
            var authorService = Injector.Create<AuthorService>();
            Assert.IsTrue(authorService.Delete());

            // Clean Domain table
            var domainService = Injector.Create<DomainService>();
            Assert.IsTrue(domainService.Delete());

            // Clean User table
            var userService = Injector.Create<UserService>();
            Assert.IsTrue(userService.Delete());

            // Clean Properties table
            var propertiesService = Injector.Create<PropertiesService>();
            Assert.IsTrue(propertiesService.Delete());
        }
    }
}

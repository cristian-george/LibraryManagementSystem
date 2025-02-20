﻿// <copyright file="BorrowServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class BorrowServiceTests.
    /// </summary>
    [TestClass]
    public class BorrowServiceTests
    {
        private BorrowService service;

        private Properties properties;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = new BorrowService();

            var propertiesService = Injector.Create<PropertiesService>();
            this.properties = ProduceModel.GetPropertiesModel();
            Assert.IsTrue(propertiesService.Insert(this.properties));
        }

        /// <summary>
        /// Defines the test method EndToEndBorrow.
        /// </summary>
        [TestMethod]
        public void EndToEndBorrow()
        {
            var author = new Author()
            {
                FirstName = "Alexandra",
                LastName = "Baicoianu",
                Books = new List<Book>(),
            };

            var rootDomain = ProduceModel.GetScienceDomainModel();
            var domain = rootDomain.ChildDomains.ElementAt(3) // Informatica
                                   .ChildDomains.ElementAt(0) // Algoritmi
                                   .ChildDomains.ElementAt(0); // Algoritmi fundamentali

            var book = new Book()
            {
                Title = "Curs algoritmica",
                Genre = "Programare",
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

            var otherStock = new Stock()
            {
                Edition = otherEdition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 15,
                NumberOfBooksForLectureOnly = 5,
                SupplyDate = DateTime.Now.AddDays(-3),
            };

            var stockService = Injector.Create<StockService>();

            Assert.IsTrue(stockService.Insert(stock));
            Assert.IsTrue(stockService.Insert(otherStock));

            var dbStock = stockService.GetById(stock.Id);
            var dbOtherStock = stockService.GetById(otherStock.Id);

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Now.AddMonths(-2),
                ReturnDate = DateTime.Now.AddMonths(-2).AddDays(14),
                Reader = reader,
                Librarian = librarian,
                Stocks = new List<Stock>() { dbStock, dbOtherStock },
            };

            // Insert
            Assert.IsTrue(this.service.Insert(borrow));

            Assert.IsTrue(reader.ReaderBorrows.Count > 0);
            Assert.IsTrue(librarian.LibrarianGrants.Count > 0);

            // GetAll
            var allBorrows = this.service.Get();
            Assert.IsNotNull(allBorrows);

            // GetById
            var id = allBorrows.LastOrDefault().Id;
            var dbBorrow = this.service.GetById(id);
            Assert.IsNotNull(dbBorrow);

            // Get borrows from every existing stock
            foreach (var s in dbBorrow.Stocks)
            {
                Assert.IsNotNull(s);
                Assert.IsTrue(s.Borrows.Count == 1);
                Assert.IsTrue(s.Borrows.Contains(dbBorrow));
            }

            // Update
            borrow.Librarian.PhoneNumber = "0770400405";
            Assert.IsTrue(this.service.Update(dbBorrow));

            // Delete
            Assert.IsTrue(this.service.DeleteById(id));
        }

        /// <summary>
        /// Defines the test method CheckCanBooksBeGrantedShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckCanBooksBeGrantedShouldReturnFalse()
        {
            var stockService = Injector.Create<StockService>();

            var borrow = ProduceModel.GetBorrowModelTwoStocks();

            foreach (var stock in borrow.Stocks)
            {
                Assert.IsTrue(stockService.Insert(stock));
            }

            Assert.IsFalse(this.service.CheckCanBooksBeGranted(borrow));
        }

        /// <summary>
        /// Defines the test method CheckCanBorrowMaxNMCInPERMonthsShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckCanBorrowMaxNMCInPERMonthsShouldReturnFalse()
        {
            var userService = Injector.Create<UserService>();
            var stockService = Injector.Create<StockService>();

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddDays(-2),
                ReturnDate = DateTime.Today.AddDays(14),
                Stocks = new List<Stock>(),
            };

            int count = 2 * this.properties.Nmc;

            // Add stocks
            for (var index = 0; index < count; ++index)
            {
                var stock = ProduceModel.GetStockModelWithPaperback();
                Assert.IsTrue(stockService.Insert(stock));

                borrow.Stocks.Add(stock);
            }

            // Add librarian
            var librarian = ProduceModel.GetLibrarianReaderModel();
            Assert.IsTrue(userService.Insert(librarian));

            // Check condition for a librarian reader
            borrow.Librarian = borrow.Reader = librarian;
            Assert.IsTrue(this.service.CheckCanBorrowMaxNMCInPERMonths(borrow));

            // Add user
            var reader = ProduceModel.GetReaderModel();
            Assert.IsTrue(userService.Insert(reader));

            // Check condition for a reader
            borrow.Reader = reader;
            Assert.IsFalse(this.service.CheckCanBorrowMaxNMCInPERMonths(borrow));
        }

        /// <summary>
        /// Defines the test method CheckBorrowedBooksForMaxCBooksShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckBorrowedBooksForMaxCBooksShouldReturnFalse()
        {
            var userService = Injector.Create<UserService>();
            var stockService = Injector.Create<StockService>();

            // Add librarian
            var librarian = ProduceModel.GetLibrarianReaderModel();
            Assert.IsTrue(userService.Insert(librarian));

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(14),
                Librarian = librarian,
                Reader = librarian,
                Stocks = new List<Stock>(),
            };

            int count = (2 * this.properties.C) - 1;

            // Add stocks
            for (var index = 0; index < count; ++index)
            {
                var stock = ProduceModel.GetStockModelWithPaperback();
                Assert.IsTrue(stockService.Insert(stock));

                borrow.Stocks.Add(stock);
            }

            Assert.IsFalse(this.service.CheckBorrowedBooksForMaxCBooks(borrow));
        }

        /// <summary>
        /// Defines the test method CheckCanBorrowAtMostDBooksInSameDomainInLastLMonthsShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckCanBorrowAtMostDBooksInSameDomainInLastLMonthsShouldReturnFalse()
        {
            var userService = Injector.Create<UserService>();
            var stockService = Injector.Create<StockService>();

            // Add librarian
            var librarian = ProduceModel.GetLibrarianModel();
            Assert.IsTrue(userService.Insert(librarian));

            // Add reader
            var reader = ProduceModel.GetReaderModel();
            Assert.IsTrue(userService.Insert(reader));

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddDays(-2),
                ReturnDate = DateTime.Today.AddDays(14),
                Librarian = librarian,
                Reader = reader,
                Stocks = new List<Stock>(),
            };

            int count = this.properties.D + 1;

            for (var index = 0; index < count; ++index)
            {
                var stock = ProduceModel.GetStockModelWithPaperback();
                Assert.IsTrue(stockService.Insert(stock));

                borrow.Stocks.Add(stock);
            }

            Assert.IsFalse(this.service.CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(borrow));
        }

        /// <summary>
        /// Defines the test method CheckBorrowExtensionAtMostLIMShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckBorrowExtensionAtMostLIMShouldReturnFalse()
        {
            var author = new Author()
            {
                FirstName = "Alexandra",
                LastName = "Baicoianu",
                Books = new List<Book>(),
            };

            var rootDomain = ProduceModel.GetScienceDomainModel();
            var domain = rootDomain.ChildDomains.ElementAt(3) // Informatica
                                   .ChildDomains.ElementAt(0) // Algoritmi
                                   .ChildDomains.ElementAt(0); // Algoritmi fundamentali

            var book = new Book()
            {
                Title = "Curs algoritmica",
                Genre = "Programare",
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

            var stock = new Stock()
            {
                Edition = edition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                SupplyDate = DateTime.Now.AddDays(-7),
            };

            var otherStock = new Stock()
            {
                Edition = otherEdition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 15,
                NumberOfBooksForLectureOnly = 5,
                SupplyDate = DateTime.Now.AddDays(-3),
            };

            var stockService = Injector.Create<StockService>();

            Assert.IsTrue(stockService.Insert(stock));
            Assert.IsTrue(stockService.Insert(otherStock));

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

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddDays(-2),
                ReturnDate = DateTime.Today.AddDays(-2).AddDays(14),
                Librarian = librarian,
                Reader = reader,
                Stocks = new List<Stock>() { stock },
            };

            Assert.IsTrue(this.service.CheckBorrowExtensionAtMostLIM(borrow));
            Assert.IsTrue(this.service.Insert(borrow));

            var otherBorrow = new Borrow()
            {
                BorrowDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(14),
                Librarian = librarian,
                Reader = reader,
                Stocks = new List<Stock>() { otherStock },
            };

            Assert.IsFalse(this.service.CheckBorrowExtensionAtMostLIM(otherBorrow));
        }

        /// <summary>
        /// Defines the test method CheckBorrowsMadeInDELTADaysShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckBorrowsMadeInDELTADaysShouldReturnFalse()
        {
            var author = new Author()
            {
                FirstName = "Alexandra",
                LastName = "Baicoianu",
                Books = new List<Book>(),
            };

            var rootDomain = ProduceModel.GetScienceDomainModel();
            var domain = rootDomain.ChildDomains.ElementAt(3) // Informatica
                                   .ChildDomains.ElementAt(0) // Algoritmi
                                   .ChildDomains.ElementAt(0); // Algoritmi fundamentali

            var book = new Book()
            {
                Title = "Curs algoritmica",
                Genre = "Programare",
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

            var stock = new Stock()
            {
                Edition = edition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                SupplyDate = DateTime.Now.AddDays(-7),
            };

            var otherStock = new Stock()
            {
                Edition = otherEdition,
                InitialStock = 20,
                NumberOfBooksForBorrowing = 15,
                NumberOfBooksForLectureOnly = 5,
                SupplyDate = DateTime.Now.AddDays(-3),
            };

            var stockService = Injector.Create<StockService>();

            Assert.IsTrue(stockService.Insert(stock));
            Assert.IsTrue(stockService.Insert(otherStock));

            var user = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "Brasov, strada Iuliu Maniu, nr. 50",
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
                UserType = EUserType.LibrarianReader,
            };

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddDays(-2),
                ReturnDate = DateTime.Today.AddDays(-2).AddDays(14),
                Librarian = user,
                Reader = user,
                Stocks = new List<Stock>() { stock },
            };

            Assert.IsTrue(this.service.Insert(borrow));

            var otherBorrow = new Borrow()
            {
                BorrowDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(14),
                Librarian = user,
                Reader = user,
                Stocks = new List<Stock>() { otherStock },
            };

            Assert.IsFalse(this.service.CheckBorrowsMadeInDELTADays(otherBorrow));
        }

        /// <summary>
        /// Defines the test method CheckCanBorrowAtMostNCZBooksInOneDayShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckCanBorrowAtMostNCZBooksInOneDayShouldReturnFalse()
        {
            var userService = Injector.Create<UserService>();
            var stockService = Injector.Create<StockService>();

            var reader = ProduceModel.GetReaderModel();
            Assert.IsTrue(userService.Insert(reader));

            var borrow = ProduceModel.GetBorrowModelWithOneStock1();
            var otherBorrow = ProduceModel.GetBorrowModelWithOneStock2();

            var stock = borrow.Stocks.First();
            var otherStock = otherBorrow.Stocks.First();

            // Update information so that a specific book edition
            // can be borrowed
            otherStock.NumberOfBooksForBorrowing = 15;
            otherBorrow.Stocks = new List<Stock>() { otherStock };

            Assert.IsTrue(stockService.Insert(stock));
            Assert.IsTrue(stockService.Insert(otherStock));

            // Get all stocks
            var dbStocks = stockService.Get();
            Assert.AreEqual(2, dbStocks.Count());

            // Insert first borrow
            borrow.Reader = reader;
            this.service.Insert(borrow);

            // Insert second borrow
            otherBorrow.Reader = reader;
            Assert.IsTrue(this.service.Insert(otherBorrow));

            // Get all borrows
            var dbBorrows = this.service.Get();
            Assert.IsTrue(dbBorrows.Count() == 2);

            var tryBorrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddMonths(-1),
                Reader = reader,
                Stocks = new List<Stock>(),
            };

            Assert.IsFalse(this.service.CheckCanBorrowAtMostNCZBooksInOneDay(tryBorrow));
        }

        /// <summary>
        /// Defines the test method CheckGrantAtMostPERSIMPBooksInOneDayShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckGrantAtMostPERSIMPBooksInOneDayShouldReturnFalse()
        {
            var userService = Injector.Create<UserService>();
            var stockService = Injector.Create<StockService>();

            var librarian = ProduceModel.GetLibrarianModel();
            Assert.IsTrue(userService.Insert(librarian));

            var borrow = ProduceModel.GetBorrowModelWithOneStock1();
            var otherBorrow = ProduceModel.GetBorrowModelWithOneStock2();

            var stock = borrow.Stocks.First();
            var otherStock = otherBorrow.Stocks.First();

            // Update information so that a specific book edition
            // can be borrowed
            otherStock.NumberOfBooksForBorrowing = 15;
            otherBorrow.Stocks = new List<Stock>() { otherStock };

            Assert.IsTrue(stockService.Insert(stock));
            Assert.IsTrue(stockService.Insert(otherStock));

            // Insert first borrow
            borrow.Librarian = librarian;
            this.service.Insert(borrow);

            // Insert second borrow
            otherBorrow.Librarian = librarian;
            Assert.IsTrue(this.service.Insert(otherBorrow));

            var tryBorrow = new Borrow()
            {
                BorrowDate = DateTime.Today.AddMonths(-1),
                Librarian = librarian,
                Stocks = new List<Stock>(),
            };

            Assert.IsFalse(this.service.CheckGrantAtMostPERSIMPBooksInOneDay(tryBorrow));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());

            // Clean Stock table
            var stockService = Injector.Create<StockService>();
            Assert.IsTrue(stockService.Delete());

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

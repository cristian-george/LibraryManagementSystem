// <copyright file="BorrowServiceTests.cs" company="Transilvania University of Brasov">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BorrowServiceTests.
    /// </summary>
    [TestClass]
    public class BorrowServiceTests
    {
        private BorrowService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = new BorrowService();
        }

        /// <summary>
        /// Defines the test method EndToEndBorrow.
        /// </summary>
        [TestMethod]
        public void EndToEndBorrow()
        {
            // Add properties
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert properties
            Assert.IsTrue(propertiesService.Insert(properties));

            // Add author
            var authorService = Injector.Create<AuthorService>();
            var bookAuthor = new Author()
            {
                FirstName = "Alexandra",
                LastName = "Baicoianu",
            };

            // Insert author
            Assert.IsTrue(authorService.Insert(bookAuthor));

            // Add domain
            var domainService = Injector.Create<DomainService>();
            var mainDomain = ProduceModel.GetScienceDomainModel();
            var bookDomain = mainDomain.ChildDomains.ElementAt(3) // Informatica
                                   .ChildDomains.ElementAt(0) // Algoritmi
                                   .ChildDomains.ElementAt(0); // Algoritmi fundamentali

            // Insert domains
            Assert.IsTrue(domainService.Insert(mainDomain));
            Assert.IsTrue(domainService.Insert(bookDomain));

            // Add editions
            var editionService = Injector.Create<EditionService>();

            var bookEdition1 = new Edition()
            {
                Publisher = "Editura Universitatii",
                Year = 2015,
                EditionNumber = 15,
                NumberOfPages = 240,
                BookType = EBookType.Hardcover,
            };

            var bookEdition2 = new Edition()
            {
                Publisher = "Editura UniTBv",
                Year = 2018,
                EditionNumber = 18,
                NumberOfPages = 270,
                BookType = EBookType.Hardcover,
            };

            // Insert editions
            Assert.IsTrue(editionService.Insert(bookEdition1));
            Assert.IsTrue(editionService.Insert(bookEdition2));

            // Add books
            var bookService = Injector.Create<BookService>();
            var book = new Book()
            {
                Title = "Curs algoritmica",
                Authors = new List<Author>() { bookAuthor },
                Domains = new List<Domain>() { bookDomain },
                Editions = new List<Edition>() { bookEdition1, bookEdition2 },
            };

            // Insert book
            Assert.IsTrue(bookService.Insert(book));
            Assert.IsTrue(bookService.Insert(ProduceModel.GetBookModel()));

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

            var allBooks = bookService.Get(
                null,
                book => book.OrderBy(x => x.Id),
                string.Empty).ToList();

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Now.AddMonths(-2),
                Reader = reader,
                Librarian = librarian,

                // Stocks = allBooks,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(borrow));

            // GetAll
            var allBorrows = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allBorrows);

            // GetById
            var id = allBorrows.LastOrDefault().Id;
            var dbBorrow = this.service.GetById(id);
            Assert.IsNotNull(dbBorrow);

            // Update
            borrow.Reader.Email = "validEmail@gmail.com";
            Assert.IsTrue(this.service.Update(dbBorrow));

            // Delete
            Assert.IsTrue(this.service.DeleteById(id));
        }

        /// <summary>
        /// Defines the test method BadInsertTest.
        /// </summary>
        [TestMethod]
        public void BadInsertTest()
        {
            var borrow = new Borrow();

            Assert.IsFalse(this.service.Insert(borrow));
        }

        /// <summary>
        /// Defines the test method CheckIfBooksAreBorrowableShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckIfBooksAreBorrowableShouldReturnFalse()
        {
            var borrow = new Borrow()
            {
                // Stocks = new List<Stock>() { TestUtils.GetBookModel() },
            };

            // Assert.IsFalse(this.service.CheckNumberOfBooksLeftIsAtLeast10Percent(borrow));
        }

        /// <summary>
        /// Defines the test method CheckIfBooksAreBorrowableShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckIfBooksAreBorrowableShouldReturnFalseWhenBorrowHasMoreThanOneBook()
        {
            var borrow = new Borrow()
            {
                // Stocks = new List<Stock>() { TestUtils.GetBookModel(), TestUtils.GetBookModel() },
            };

            // Assert.IsFalse(this.service.CheckIfAtLeastABookIsForLecture(borrow));
        }

        /// <summary>
        /// Defines the test method CheckLIMShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckLIMShouldReturnFalse()
        {
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            // List of books
            var books = new List<Book>()
            {
                ProduceModel.GetBookModel(),
                ProduceModel.GetBookModel(),
            };

            // Properties LIM value
            var value = propertiesService.Get().LastOrDefault().Lim;

            var borrow = new Borrow
            {
                // Stocks = books,
                // NoOfTimeExtended = value,
            };

            // Check if it is possible to extend the borrow time
            Assert.IsFalse(this.service.CheckBorrowExtensionAtMostLIM(borrow));
        }

        /// <summary>
        /// Defines the test method CheckLIMShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void CheckLIMShouldReturnTrue()
        {
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            // List of books
            var books = new List<Book>()
            {
                ProduceModel.GetBookModel(),
                ProduceModel.GetBookModel(),
            };

            // Properties Lim value
            var value = propertiesService.Get().LastOrDefault().Lim;

            var borrow = new Borrow
            {
                // Stocks = books,
                // NoOfTimeExtended = value - 1,
            };

            // Check if it is possible to extend the borrow time
            Assert.IsTrue(this.service.CheckBorrowExtensionAtMostLIM(borrow));
        }

        /// <summary>
        /// Defines the test method GetParentDomainShouldReturnCorrectParentDomain.
        /// </summary>
        [TestMethod]
        public void GetParentDomainShouldReturnCorrectParentDomain()
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

            // Assert.AreEqual(parent, DomainServiceUtils.GetRootDomain(children));
        }

        /// <summary>
        /// Defines the test method GetNoOfDistinctCategoriesShouldReturns1.
        /// </summary>
        [TestMethod]
        public void GetNoOfDistinctCategoriesShouldReturns1()
        {
            var d1 = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
            };

            var d2 = new Domain()
            {
                Name = "Literatura",
                ParentDomain = null,
            };

            var d3 = new Domain()
            {
                Name = "Medicina",
                ParentDomain = null,
            };

            var listOfDomains = new List<Domain>() { d1, d2, d3 };

            // Assert.AreEqual(3, DomainServiceUtils.GetNoOfDistinctDomains(listOfDomains));
        }

        /// <summary>
        /// Defines the test method CheckMaxBorrowBooksTodayShouldReturnTrueWhenBorrowsTableIsEmpty.
        /// </summary>
        [TestMethod]
        public void CheckMaxBorrowBooksTodayShouldReturnTrueWhenBorrowsTableIsEmpty()
        {
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

            var borrow = ProduceModel.GetBorrowModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            Assert.IsTrue(this.service.CheckCanBorrowAtMostNCZBooksToday(borrow));
        }

        /// <summary>
        /// Defines the test method CheckCanBorrowMaxNMCInPERShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void CheckCanBorrowMaxNMCInPERShouldReturnFalse()
        {
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                Domenii = 2,
                Nmc = 2,
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

            // Create and add borrows

            // Insert 30 books
            var bookService = Injector.Create<BookService>();
            var listOfBooks = ProduceModel.GetListOFSameBook(30);
            foreach (var bookToBeInserted in listOfBooks)
            {
                // Assert if books are added succesfully
                Assert.IsTrue(bookService.Insert(bookToBeInserted));
            }

            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
            };

            var librarian = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                PhoneNumber = "0734525427",
                Email = "test_test@gmail.com",
                UserType = EUserType.LibrarianReader,
            };

            // Add books that will be borrowed
            var listOfBooksToBeBorrowed = new List<Book>();
            var allBooks = bookService.Get(null, book => book.OrderBy(x => x.Id), string.Empty).ToList();

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Now.AddMonths(-2),
                ReturnDate = DateTime.Now.AddMonths(3),
                Reader = librarian,
                Librarian = librarian,
                Stocks = new List<Stock>(),
            };

            // Update
            // borrow.Stocks.Add(allBooks.FirstOrDefault());
            // borrow.Stocks.Add(allBooks.LastOrDefault());
            borrow.Reader.Email = "validEmail@gmail.com";

            Assert.IsFalse(this.service.CheckCanBorrowMaxNMCInPER(borrow));
        }

        /// <summary>
        /// Defines the test method CheckBorrowedBooksForMaxCBooksShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void CheckBorrowedBooksForMaxCBooksShouldReturnTrue()
        {
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            var book1 = ProduceModel.GetBookModel();
            var book2 = ProduceModel.GetBookModel();
            book2.Domains.First().Name = "Literatura";
            var book3 = ProduceModel.GetBookModel();
            book3.Domains.First().Name = "Literatura";

            var borrow = new Borrow()
            {
                // Stocks = new List<Stock>() { book1, book2, book3 },
            };

            Assert.IsTrue(this.service.CheckBorrowedBooksForMaxCBooks(borrow));
        }

        /// <summary>
        /// Defines the test method CheckBorrowInDELTATimeShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void CheckBorrowInDELTATimeShouldReturnTrue()
        {
            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            // Insert 30 books
            var bookService = Injector.Create<BookService>();
            var listOfBooks = ProduceModel.GetListOFSameBook(30);
            foreach (var bookToBeInserted in listOfBooks)
            {
                // Assert if books are added succesfully
                Assert.IsTrue(bookService.Insert(bookToBeInserted));
            }

            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
            };

            var librarian = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                PhoneNumber = "0734525427",
                Email = "test_test@gmail.com",
                UserType = EUserType.LibrarianReader,
            };

            // Add books that will be borrowed
            var listOfBooksToBeBorrowed = new List<Book>();
            var allBooks = bookService.Get(null, book => book.OrderBy(x => x.Id), string.Empty).ToList();

            var borrow = new Borrow()
            {
                BorrowDate = DateTime.Now.AddMonths(-2),
                ReturnDate = DateTime.Now.AddMonths(3),
                Reader = librarian,
                Librarian = librarian,
                Stocks = new List<Stock>(),
            };

            // Update
            //borrow.Stocks.Add(allBooks.LastOrDefault());
            borrow.Reader.Email = "validEmail@gmail.com";

            Assert.IsTrue(this.service.CheckBorrowInDELTATime(borrow));
        }

        /// <summary>
        /// Defines the test method TestCheckBorrowedBooksForMaxCBooks.
        /// </summary>
        [TestMethod]
        public void TestCheckBorrowedBooksForMaxCBooks()
        {
            var borrow = new Borrow()
            {
                Reader = new User()
                {
                },
                Stocks = new List<Stock>()
                {
                    new (),
                    new (),
                    new (),
                    new (),
                    new (),
                },
            };

            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                Domenii = 2,
                Nmc = 2,
                L = 2,
                C = 1,
                D = 1,
                Lim = 2,
                Delta = 3,
                Ncz = 4,
                Persimp = 3,
                Per = 3,
            };

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            Assert.IsFalse(this.service.CheckBorrowedBooksForMaxCBooks(borrow));

            _ = borrow.Stocks.Remove(new Stock());
            borrow.Stocks = new List<Stock>();

            Assert.IsTrue(this.service.CheckBorrowedBooksForMaxCBooks(borrow));
        }

        /// <summary>
        /// Defines the test method TestCheckIfBooksAreBorrowable.
        /// </summary>
        [TestMethod]
        public void TestCheckIfBooksAreBorrowable()
        {
            var borrow = new Borrow()
            {
                Reader = new User()
                {
                },
                Stocks = new List<Stock>()
                {
                    new ()
                    {
                    },
                },
            };

            // Assert.IsFalse(this.service.CheckBorrowAdditionalRules(borrow));
        }

        /// <summary>
        /// Defines the test method TestCheckGrantAtMostPERSIMPBooksToday.
        /// </summary>
        [TestMethod]
        public void TestCheckGrantAtMostPERSIMPBooksToday()
        {
            var borrow = new Borrow()
            {
                Reader = new User()
                {
                },
                Stocks = new List<Stock>()
                {
                    new ()
                    {
                    },
                },
            };

            var propertiesService = Injector.Create<PropertiesService>();
            var properties = ProduceModel.GetPropertiesModel();

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            Assert.IsTrue(this.service.CheckGrantAtMostPERSIMPBooksToday(borrow));
        }

        /// <summary>
        /// Defines the test method TestCheckBorrowInDELTATime.
        /// </summary>
        [TestMethod]
        public void TestCheckBorrowInDELTATime()
        {
            var user = new User()
            {
                UserType = EUserType.LibrarianReader,
            };

            var borrow = new Borrow()
            {
                Librarian = user,
                Reader = user,
                Stocks = new List<Stock>()
                {
                    new (),
                },
            };

            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                Domenii = 2,
                Nmc = 2,
                L = 2,
                C = 1,
                D = 1,
                Lim = 2,
                Delta = 3,
                Ncz = 4,
                Persimp = 3,
                Per = 3,
            };

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            Assert.IsTrue(this.service.CheckBorrowInDELTATime(borrow));
        }

        /// <summary>
        /// Defines the test method TestCheckCanBorrowMaxNMCInPER.
        /// </summary>
        [TestMethod]
        public void TestCheckCanBorrowMaxNMCInPER()
        {
            var user = new User()
            {
                UserType = EUserType.LibrarianReader,
            };

            var borrow = new Borrow()
            {
                Reader = user,
                Librarian = user,
                Stocks = new List<Stock>()
                {
                    new (),
                },
            };

            var propertiesService = Injector.Create<PropertiesService>();
            var properties = new Properties()
            {
                Domenii = 2,
                Nmc = 2,
                L = 2,
                C = 1,
                D = 1,
                Lim = 2,
                Delta = 3,
                Ncz = 4,
                Persimp = 3,
                Per = 3,
            };

            // Insert Properties
            Assert.IsTrue(propertiesService.Insert(properties));

            Assert.IsTrue(this.service.CheckCanBorrowMaxNMCInPER(borrow));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());

            // Clean Librarian table
            var librarianService = Injector.Create<UserService>();
            Assert.IsTrue(librarianService.Delete());

            // Clean Reader table
            var readerService = Injector.Create<UserService>();
            Assert.IsTrue(readerService.Delete());

            // Clean Author table
            var authorService = Injector.Create<AuthorService>();
            Assert.IsTrue(authorService.Delete());

            // Clean Domain table
            var domainService = Injector.Create<DomainService>();
            Assert.IsTrue(domainService.Delete());

            // Clean Edition table
            var editionService = Injector.Create<EditionService>();
            Assert.IsTrue(editionService.Delete());

            // Clean Account table
            var accountService = Injector.Create<StockService>();
            Assert.IsTrue(accountService.Delete());

            // Clean Properties table
            var propertiesService = Injector.Create<PropertiesService>();
            Assert.IsTrue(propertiesService.Delete());

            // Clean Book table
            var bookService = Injector.Create<BookService>();
            Assert.IsTrue(bookService.Delete());
        }
    }
}

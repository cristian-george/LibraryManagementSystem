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
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BookServiceTests.
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        private BookService service;

        private Properties properties;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<BookService>();

            var propertiesService = Injector.Create<PropertiesService>();
            this.properties = ProduceModel.GetPropertiesModel();
            Assert.IsTrue(propertiesService.Insert(this.properties));
        }

        /// <summary>
        /// Defines the test method EndToEndBook.
        /// </summary>
        [TestMethod]
        public void EndToEndBook()
        {
            var domain = ProduceModel.GetDomainWithSubdomainModel();

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
                Domains = new List<Domain>() { domain },
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
        /// Defines the test method InsertSameBooksShouldReturnTrue.
        /// </summary>
        [TestMethod]
        public void InsertSameBooksShouldReturnTrue()
        {
            // Clean Book table
            _ = this.service.Delete();

            var books = ProduceModel.GetListOFSameBook(100);

            var book = books.First();
            Assert.IsTrue(this.service.Insert(book));

            books.RemoveAt(0);

            for (int i = 0; i < books.Count; i++)
            {
                var letterCombination = GetLetterCombination(i);
                books[i].Title += letterCombination;
                Assert.IsTrue(this.service.Insert(books[i]));
            }
        }

        /// <summary>
        /// Defines the test method InsertSameBooksShouldReturnFalse.
        /// </summary>
        [TestMethod]
        public void InsertSameBooksShouldReturnFalse()
        {
            // Clean Book table
            _ = this.service.Delete();

            var books = ProduceModel.GetListOFSameBook(100);

            var book = books.First();
            Assert.IsTrue(this.service.Insert(book));

            books.RemoveAt(0);
            books.ForEach(book =>
                Assert.IsFalse(this.service.Insert(book)));
        }

        /// <summary>
        /// Defines the test method IsInParentChildRelationDomainsShouldReturnTrue.
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
        /// Defines the test method IsInParentChildRelationDomainsShouldReturnFalse.
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
        /// Defines the test method IsInMoreThanNDomainsShouldReturnTrue.
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
                Name = "Stiinta3",
            };

            var book = new Book()
            {
                Domains = new List<Domain>() { domain1, domain2, domain3 },
            };

            Assert.IsTrue(this.service.IsInMoreThanNDomains(book));
        }

        /// <summary>
        /// Defines the test method IsInMoreThanNDomainsShouldReturnFalse.
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

        /// <summary>
        /// Generates letter combinations similar to "A, B, ..., Z, AA, AB, ..., ZZ, AAA, ...".
        /// </summary>
        /// <param name="index">The zero-based index for the letter combination (0 = "A", 1 = "B", ..., 25 = "Z", 26 = "AA").</param>
        /// <returns>A string representing the letter combination.</returns>
        private static string GetLetterCombination(int index)
        {
            string result = string.Empty;

            while (index >= 0)
            {
                result = (char)('A' + (index % 26)) + result;
                index = (index / 26) - 1;
            }

            return result;
        }
    }
}

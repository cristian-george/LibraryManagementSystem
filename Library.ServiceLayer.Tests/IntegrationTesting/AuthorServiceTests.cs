// <copyright file="AuthorServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class AuthorServiceTests.
    /// </summary>
    [TestClass]
    public class AuthorServiceTests
    {
        private AuthorService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<AuthorService>();
        }

        /// <summary>
        /// Defines the test method EndToEndAuthor.
        /// </summary>
        [TestMethod]
        public void EndToEndAuthor()
        {
            var book = new Book()
            {
                Title = "Test",
                Genre = "Test",
                Domains = new List<Domain>()
                {
                    new Domain()
                    {
                        Name = "Domeniu",
                    },
                },
                Authors = new List<Author>(),
            };
            var author = new Author()
            {
                FirstName = "Marcel",
                LastName = "Dorel",
                Books = new List<Book>(),
            };

            author.Books.Add(book);
            book.Authors.Add(author);

            // Insert
            Assert.IsTrue(this.service.Insert(author));

            // GetById intr-un fel, din cauza ca adauga prea multe in baza de date..
            var dbAccount = this.service.Get(null, null, string.Empty).LastOrDefault();
            Assert.IsNotNull(dbAccount);
            Assert.IsNotNull(this.service.GetById(dbAccount.Id));

            // GetAll
            var allAccounts = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allAccounts);

            // Update
            dbAccount.LastName = "Garcea";
            Assert.IsTrue(this.service.Update(dbAccount));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbAccount.Id));
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

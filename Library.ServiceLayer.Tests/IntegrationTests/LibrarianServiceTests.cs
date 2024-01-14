﻿// <copyright file="LibrarianServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTests
{
    using System.Linq;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class LibrarianServiceTests.
    /// </summary>
    [TestClass]
    public class LibrarianServiceTests
    {
        private LibrarianService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<LibrarianService>();
        }

        /// <summary>
        /// Ends to end librarian.
        /// </summary>
        [TestMethod]
        public void EndToEndLibrarian()
        {
            var service = Injector.Create<LibrarianService>();

            var account = new Account()
            {
                PhoneNumber = "0734525427",
                Email = "gogumortu@gmail.com",
            };

            var librarian = new Librarian()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                IsReader = true,
                Account = account,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(librarian));

            // GetById intr-un fel, din cauza ca adauga prea multe in baza de date..
            var dbLibrarian = this.service.Get(null, null, string.Empty).LastOrDefault();
            Assert.IsNotNull(dbLibrarian);
            Assert.IsNotNull(this.service.GetById(dbLibrarian.Id));

            // GetAll
            var allLibrarians = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allLibrarians);

            // Update
            dbLibrarian.Account.Email = "mihaialex@gmail.com";
            dbLibrarian.FirstName = "Garcea";
            Assert.IsTrue(this.service.Update(dbLibrarian));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbLibrarian.Id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean account table
            var accountService = Injector.Create<AccountService>();
            Assert.IsTrue(accountService.Delete());

            // Clean table
            Assert.IsTrue(this.service.Delete());
        }
    }
}

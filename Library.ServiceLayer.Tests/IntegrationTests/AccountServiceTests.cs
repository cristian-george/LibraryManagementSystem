// <copyright file="AccountServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Class AccountServiceTests.
    /// </summary>
    [TestClass]
    public class AccountServiceTests
    {
        private StockService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<StockService>();
        }

        /// <summary>
        /// Defines the test method EndToEndAccount.
        /// </summary>
        [TestMethod]
        public void EndToEndAccount()
        {
            var account = new Account()
            {
                PhoneNumber = "1234567890",
                Email = "eunmailvalid@gmail.com",
            };

            // Insert
            Assert.IsTrue(this.service.Insert(account));

            // GetAll
            var allAccounts = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allAccounts);

            // GetById
            var id = allAccounts.LastOrDefault().Id;
            var dbAccount = this.service.GetById(id);
            Assert.IsNotNull(dbAccount);

            // Update
            dbAccount.Email = "validmain123@mail.com";
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
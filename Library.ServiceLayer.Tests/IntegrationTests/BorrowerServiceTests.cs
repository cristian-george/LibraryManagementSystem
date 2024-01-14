// <copyright file="BorrowerServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class BorrowerServiceTests.
    /// </summary>
    [TestClass]
    public class BorrowerServiceTests
    {
        private BorrowerService service;

        private Account account = null;
        private Borrower borrower = null;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<BorrowerService>();

            this.account = new Account()
            {
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
            };

            this.borrower = new Borrower()
            {
                LastName = "Fieraru",
                FirstName = "Cristian",
                Address = "Brasov, strada Iuliu Maniu, nr. 50",
                Account = this.account,
            };
        }

        /// <summary>
        /// Defines the test method EndToEndBorrower.
        /// </summary>
        [TestMethod]
        public void EndToEndBorrower()
        {
            // Insert
            Assert.IsTrue(this.service.Insert(this.borrower));

            // GetAll
            var allBorrowers = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allBorrowers);

            // GetById
            var id = allBorrowers.LastOrDefault().Id;
            var dbBorrower = this.service.GetById(id);
            Assert.IsNotNull(dbBorrower);

            // Update
            dbBorrower.Address = "Brasov, strada Iuliu Maniu, nr. 1";
            dbBorrower.Account.Email = "cristian.fieraru01@gmail.com";
            Assert.IsTrue(this.service.Update(dbBorrower));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbBorrower.Id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean borrower table
            Assert.IsTrue(this.service.Delete());

            // Clean account table
            var accountService = Injector.Create<AccountService>();
            Assert.IsTrue(accountService.Delete());
        }
    }
}

// <copyright file="ReaderServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class ReaderServiceTests.
    /// </summary>
    [TestClass]
    public class ReaderServiceTests
    {
        private ReaderService service;

        private Account account = null;
        private Reader reader = null;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<ReaderService>();

            this.account = new Account()
            {
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
            };

            this.reader = new Reader()
            {
                LastName = "Fieraru",
                FirstName = "Cristian",
                Address = "Brasov, strada Iuliu Maniu, nr. 50",
                Account = this.account,
            };
        }

        /// <summary>
        /// Defines the test method EndToEndReader.
        /// </summary>
        [TestMethod]
        public void EndToEndReader()
        {
            // Insert
            Assert.IsTrue(this.service.Insert(this.reader));

            // GetAll
            var allReaders = this.service.Get(null, null, string.Empty);
            Assert.IsNotNull(allReaders);

            // GetById
            var id = allReaders.LastOrDefault().Id;
            var dbReader = this.service.GetById(id);
            Assert.IsNotNull(dbReader);

            // Update
            dbReader.Address = "Brasov, strada Iuliu Maniu, nr. 1";
            dbReader.Account.Email = "cristian.fieraru01@gmail.com";
            Assert.IsTrue(this.service.Update(dbReader));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbReader.Id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean reader table
            Assert.IsTrue(this.service.Delete());

            // Clean account table
            var accountService = Injector.Create<AccountService>();
            Assert.IsTrue(accountService.Delete());
        }
    }
}

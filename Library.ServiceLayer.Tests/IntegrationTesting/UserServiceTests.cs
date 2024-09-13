// <copyright file="UserServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System.Linq;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class AccountServiceTests.
    /// </summary>
    [TestClass]
    public class UserServiceTests
    {
        private UserService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<UserService>();
        }

        /// <summary>
        /// Defines the test method EndToEndAccount.
        /// </summary>
        [TestMethod]
        public void EndToEndUser()
        {
            var user = new User()
            {
                FirstName = "User",
                LastName = "User",
                Address = "str. Adresa, nr. 30",
                PhoneNumber = "1234567890",
                Email = "validmail@gmail.com",
                UserType = DomainLayer.Enums.EUserType.Reader,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(user));

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
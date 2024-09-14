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
        /// Defines the test method EndToEndReader.
        /// </summary>
        [TestMethod]
        public void EndToEndReader()
        {
            var user = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                PhoneNumber = "1234567890",
                Email = "cristian.fieraru@gmail.com",
                UserType = DomainLayer.Enums.EUserType.Reader,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(user));

            // GetAll
            var allUsers = this.service.Get();
            Assert.IsNotNull(allUsers);

            // GetById
            var id = allUsers.LastOrDefault().Id;
            var dbUser = this.service.GetById(id);
            Assert.IsNotNull(dbUser);

            // Update
            dbUser.Email = "validmain123@mail.com";
            Assert.IsTrue(this.service.Update(dbUser));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbUser.Id));
        }

        /// <summary>
        /// Defines the test method EndToEndLibrarian.
        /// </summary>
        [TestMethod]
        public void EndToEndLibrarian()
        {
            var service = Injector.Create<UserService>();

            var librarian = new User()
            {
                LastName = "Biblioteca",
                FirstName = "Judeteana",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                PhoneNumber = "0734525427",
                Email = "biblioteca_judeteana@gmail.com",
                UserType = DomainLayer.Enums.EUserType.Librarian,
            };

            // Insert
            Assert.IsTrue(this.service.Insert(librarian));

            // GetAll
            var allUsers = this.service.Get();
            Assert.IsNotNull(allUsers);

            // GetById
            var id = allUsers.LastOrDefault().Id;
            var dbUser = this.service.GetById(id);
            Assert.IsNotNull(dbUser);

            // Update
            dbUser.Email = "mihaialex@gmail.com";
            dbUser.FirstName = "Garcea";
            Assert.IsTrue(this.service.Update(dbUser));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbUser.Id));
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
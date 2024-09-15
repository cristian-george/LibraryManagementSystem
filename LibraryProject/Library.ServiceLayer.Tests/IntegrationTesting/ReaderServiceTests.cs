// <copyright file="ReaderServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System.Linq;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class ReaderServiceTests.
    /// </summary>
    [TestClass]
    public class ReaderServiceTests
    {
        private UserService service;

        private User reader = null;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<UserService>();

            this.reader = new User()
            {
                LastName = "Fieraru",
                FirstName = "Cristian",
                Address = "Brasov, strada Iuliu Maniu, nr. 50",
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
                UserType = EUserType.Reader,
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
            dbReader.Email = "cristian.fieraru01@gmail.com";
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
            // Clean table
            Assert.IsTrue(this.service.Delete());
        }
    }
}

// <copyright file="PropertiesServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class PropertyServiceTests.
    /// </summary>
    [TestClass]
    public class PropertiesServiceTests
    {
        private PropertiesService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<PropertiesService>();
        }

        /// <summary>
        /// Defines the test method EndToEndProperties.
        /// </summary>
        [TestMethod]
        public void EndToEndProperties()
        {
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

            // Insert
            Assert.IsTrue(this.service.Insert(properties));

            // GetById
            var dbProperties = this.service.Get().LastOrDefault();
            Assert.IsNotNull(dbProperties);
            Assert.IsNotNull(this.service.GetById(dbProperties.Id));

            // GetAll
            var allProperties = this.service.Get();
            Assert.IsNotNull(allProperties);

            // Update
            properties.Nmc = 2;
            properties.Per = 8;
            Assert.IsTrue(this.service.Update(dbProperties));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbProperties.Id));
        }

        /// <summary>
        /// Defines the test method InsertBadData.
        /// </summary>
        [TestMethod]
        public void InsertBadData()
        {
            var properties = new Properties()
            {
                Domenii = -2,
            };

            Assert.IsFalse(this.service.Insert(properties));
        }

        /// <summary>
        /// Defines the test method UpdateBadData.
        /// </summary>
        [TestMethod]
        public void UpdateBadData()
        {
            var properties = new Properties()
            {
                Domenii = -2,
            };

            Assert.IsFalse(this.service.Update(properties));
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

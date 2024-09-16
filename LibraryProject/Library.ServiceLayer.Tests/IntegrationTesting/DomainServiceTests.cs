// <copyright file="DomainServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class DomainServiceTests.
    /// </summary>
    [TestClass]
    public class DomainServiceTests
    {
        private DomainService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<DomainService>();
        }

        /// <summary>
        /// Ends to end domain.
        /// </summary>
        [TestMethod]
        public void EndToEndDomain()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());

            var domain = new Domain()
            {
                Name = "Literatura",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };

            var otherDomain = new Domain()
            {
                Name = "Literatura",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };

            // Insert
            Assert.IsTrue(this.service.Insert(domain));
            Assert.IsFalse(this.service.Insert(otherDomain));

            // GetById
            var dbDomain = this.service.Get().LastOrDefault();
            Assert.IsNotNull(dbDomain);
            Assert.IsNotNull(this.service.GetById(dbDomain.Id));

            // GetAll
            var allDomains = this.service.Get(null, null);
            Assert.IsNotNull(allDomains);

            // Update
            Assert.IsNotNull(dbDomain.ChildDomains);
            dbDomain.ChildDomains.Add(
                new Domain()
                {
                    Name = "Proza",
                    ParentDomain = domain,
                    ChildDomains = new List<Domain>(),
                });

            dbDomain.Name = "Stiinta";
            Assert.IsTrue(this.service.Update(dbDomain));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbDomain.Id));
        }

        /// <summary>
        /// Defines the test method InsertDomainInvalid.
        /// </summary>
        [TestMethod]
        public void InsertDomainInvalid()
        {
            var domain = new Domain()
            {
                Name = string.Empty,
                ParentDomain = null,
                ChildDomains = null,
            };

            Assert.IsFalse(this.service.Insert(domain));
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

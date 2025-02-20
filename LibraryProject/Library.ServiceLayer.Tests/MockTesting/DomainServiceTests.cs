﻿// <copyright file="DomainServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.MockTesting
{
    using System.Collections.Generic;
    using Library.DomainLayer.Models;
    using Library.ServiceLayer.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Defines test class DomainServiceTests.
    /// </summary>
    [TestClass]
    public class DomainServiceTests
    {
        /// <summary>
        /// The domain service mock.
        /// </summary>
        private Mock<IDomainService> domainServiceMock;

        /// <summary>
        /// The domain service.
        /// </summary>
        private IDomainService domainService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.domainServiceMock = new Mock<IDomainService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };

            _ = this.domainServiceMock.Setup(x => x.Insert(domain)).Returns(true);
            this.domainService = this.domainServiceMock.Object;

            var result = this.domainService.Insert(domain);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };

            _ = this.domainServiceMock.Setup(x => x.Get(null, null))
                .Returns(
                new List<Domain>()
                { domain });

            this.domainService = this.domainServiceMock.Object;

            var result = this.domainService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            var domain = new Domain()
            {
                Id = 1,
                Name = "Stiinta",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };
            _ = this.domainServiceMock.Setup(x => x.GetById(1))
                .Returns(domain);

            this.domainService = this.domainServiceMock.Object;

            var result = this.domainService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Stiinta", result.Name);
            Assert.IsNull(result.ParentDomain);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
                ChildDomains = new List<Domain>(),
            };

            _ = this.domainServiceMock.Setup(x => x.GetById(1))
                .Returns(domain);

            this.domainService = this.domainServiceMock.Object;

            var modifiedAccount = this.domainService.GetById(1);
            modifiedAccount.Name = "Filozofie";

            _ = this.domainServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.domainService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.domainServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.domainService = this.domainServiceMock.Object;

            var result = this.domainService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
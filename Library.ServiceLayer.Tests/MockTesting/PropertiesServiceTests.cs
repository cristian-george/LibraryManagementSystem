// <copyright file="PropertiesServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class PropertyServiceTests.
    /// </summary>
    [TestClass]
    public class PropertiesServiceTests
    {
        /// <summary>
        /// The properties service mock.
        /// </summary>
        private Mock<IPropertiesService> propertiesServiceMock;

        /// <summary>
        /// The properties service.
        /// </summary>
        private IPropertiesService propertiesService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.propertiesServiceMock = new Mock<IPropertiesService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
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
            };

            _ = this.propertiesServiceMock.Setup(x => x.Insert(properties)).Returns(true);
            this.propertiesService = this.propertiesServiceMock.Object;

            var result = this.propertiesService.Insert(properties);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.propertiesServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<Properties>()
                {
                    new ()
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
                    },
                });

            this.propertiesService = this.propertiesServiceMock.Object;

            var result = this.propertiesService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            _ = this.propertiesServiceMock.Setup(x => x.GetById(1))
                .Returns(new Properties
                {
                    Id = 1,
                    Domenii = 2,
                    Nmc = 3,
                    L = 2,
                    C = 3,
                    D = 2,
                    Lim = 2,
                    Delta = 3,
                    Ncz = 4,
                    Persimp = 3,
                });

            this.propertiesService = this.propertiesServiceMock.Object;

            var result = this.propertiesService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(2, result.Domenii);
            Assert.AreEqual(3, result.Nmc);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            _ = this.propertiesServiceMock.Setup(x => x.GetById(1))
                .Returns(new Properties
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
                });

            this.propertiesService = this.propertiesServiceMock.Object;

            var modifiedAccount = this.propertiesService.GetById(1);
            modifiedAccount.Domenii = 7;

            _ = this.propertiesServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.propertiesService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.propertiesServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.propertiesService = this.propertiesServiceMock.Object;

            var result = this.propertiesService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
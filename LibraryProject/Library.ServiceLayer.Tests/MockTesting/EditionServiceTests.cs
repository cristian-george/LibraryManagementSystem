// <copyright file="EditionServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class EditionServiceTests.
    /// </summary>
    [TestClass]
    public class EditionServiceTests
    {
        /// <summary>
        /// The edition service mock.
        /// </summary>
        private Mock<IEditionService> editionServiceMock;

        /// <summary>
        /// The edition service.
        /// </summary>
        private IEditionService editionService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.editionServiceMock = new Mock<IEditionService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var edition = new Edition()
            {
                Publisher = "Editura Universitatii",
                Year = 1999,
                EditionNumber = int.MaxValue,
                NumberOfPages = 1,
            };

            _ = this.editionServiceMock.Setup(x => x.Insert(edition)).Returns(true);
            this.editionService = this.editionServiceMock.Object;

            var result = this.editionService.Insert(edition);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.editionServiceMock.Setup(x => x.Get(null, null))
                .Returns(
                new List<Edition>()
                {
                    new ()
                    {
                        Publisher = "Editura Universitatii",
                        Year = 1999,
                        EditionNumber = int.MaxValue,
                        NumberOfPages = 1,
                    },
                });

            this.editionService = this.editionServiceMock.Object;

            var result = this.editionService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            _ = this.editionServiceMock.Setup(x => x.GetById(1))
                .Returns(new Edition
                {
                    Id = 1,
                    Publisher = "Editura Universitatii",
                    Year = 1999,
                    EditionNumber = int.MaxValue,
                    NumberOfPages = 1,
                });

            this.editionService = this.editionServiceMock.Object;

            var result = this.editionService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, result.NumberOfPages);
            Assert.AreEqual("Editura Universitatii", result.Publisher);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            _ = this.editionServiceMock.Setup(x => x.GetById(1))
                .Returns(new Edition
                {
                    Publisher = "Editura Universitatii",
                    Year = 1999,
                    EditionNumber = 10,
                    NumberOfPages = 1,
                });

            this.editionService = this.editionServiceMock.Object;

            var edition = this.editionService.GetById(1);
            edition.EditionNumber = 5;

            _ = this.editionServiceMock.Setup(x => x.Update(edition)).Returns(true);

            var result = this.editionService.Update(edition);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.editionServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.editionService = this.editionServiceMock.Object;

            var result = this.editionService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
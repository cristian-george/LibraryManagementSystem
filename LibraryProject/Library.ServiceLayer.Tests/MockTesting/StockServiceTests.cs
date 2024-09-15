// <copyright file="StockServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.MockTesting
{
    using System;
    using System.Collections.Generic;
    using Library.DomainLayer.Models;
    using Library.ServiceLayer.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Defines test class StockServiceTests.
    /// </summary>
    [TestClass]
    public class StockServiceTests
    {
        /// <summary>
        /// The stock service mock.
        /// </summary>
        private Mock<IStockService> stockServiceMock;

        /// <summary>
        /// The edition service.
        /// </summary>
        private IStockService stockService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.stockServiceMock = new Mock<IStockService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var stock = new Stock()
            {
                SupplyDate = DateTime.Today.AddMonths(-1),
                InitialStock = 40,
                NumberOfBooksForLectureOnly = 20,
                NumberOfBooksForBorrowing = 20,
            };

            _ = this.stockServiceMock.Setup(x => x.Insert(stock)).Returns(true);
            this.stockService = this.stockServiceMock.Object;

            var result = this.stockService.Insert(stock);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.stockServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<Stock>()
                {
                    new ()
                    {
                        SupplyDate = DateTime.Today.AddMonths(-1),
                        InitialStock = 40,
                        NumberOfBooksForLectureOnly = 20,
                        NumberOfBooksForBorrowing = 20,
                    },
                });

            this.stockService = this.stockServiceMock.Object;

            var result = this.stockService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            _ = this.stockServiceMock.Setup(x => x.GetById(1))
                .Returns(new Stock
                {
                    Id = 1,
                    SupplyDate = DateTime.Today.AddMonths(-1),
                    InitialStock = 40,
                    NumberOfBooksForLectureOnly = 20,
                    NumberOfBooksForBorrowing = 20,
                });

            this.stockService = this.stockServiceMock.Object;

            var result = this.stockService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(40, result.InitialStock);
            Assert.AreEqual(20, result.NumberOfBooksForLectureOnly);
            Assert.AreEqual(20, result.NumberOfBooksForBorrowing);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            _ = this.stockServiceMock.Setup(x => x.GetById(1))
                .Returns(new Stock
                {
                    SupplyDate = DateTime.Today.AddMonths(-1),
                    InitialStock = 40,
                    NumberOfBooksForLectureOnly = 20,
                    NumberOfBooksForBorrowing = 20,
                });

            this.stockService = this.stockServiceMock.Object;

            var stock = this.stockService.GetById(1);
            stock.NumberOfBooksForBorrowing = 5;

            _ = this.stockServiceMock.Setup(x => x.Update(stock)).Returns(true);

            var result = this.stockService.Update(stock);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.stockServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.stockService = this.stockServiceMock.Object;

            var result = this.stockService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}

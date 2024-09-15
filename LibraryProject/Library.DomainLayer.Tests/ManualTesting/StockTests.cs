// <copyright file="StockTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System;
    using System.Collections.Generic;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class ReaderTests.
    /// </summary>
    [TestClass]
    public class StockTests
    {
        /// <summary>
        /// The stock.
        /// </summary>
        private Stock stock;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var book = new Book()
            {
                Title = "Limba si literatura romana, clasa a XII-a",
                Genre = "Manual scolar",
                Domains = new List<Domain>()
                    {
                        new ()
                        {
                            Id = 1,
                            Name = "Literatura",
                        },
                    },
            };

            var edition = new Edition()
            {
                Book = book,
                BookType = EBookType.Hardcover,
                Publisher = "Editura Litera",
                Year = 2020,
                EditionNumber = 5,
                NumberOfPages = 110,
            };

            this.stock = new Stock()
            {
                SupplyDate = DateTime.Today.AddDays(-7),
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                InitialStock = 20,
                Edition = edition,
            };
        }

        /// <summary>
        /// Defines the test method SupplyDateShouldBeValid.
        /// </summary>
        [TestMethod]
        public void SupplyDateShouldBeValid()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            Assert.IsTrue(tomorrow > this.stock.SupplyDate);
        }

        /// <summary>
        /// Defines the test method SupplyDateShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void SupplyDateShouldBeInvalid()
        {
            this.stock.SupplyDate = DateTime.Today.AddDays(7);

            var tomorrow = DateTime.Today.AddDays(1);
            Assert.IsFalse(tomorrow > this.stock.SupplyDate);
        }

        /// <summary>
        /// Defines the test method NumberOfBooksForBorrowingShouldBeValid.
        /// </summary>
        [TestMethod]
        public void NumberOfBooksForBorrowingShouldBeValid()
        {
            Assert.IsTrue(this.stock.NumberOfBooksForBorrowing >= 0);
        }

        /// <summary>
        /// Defines the test method NumberOfBooksForBorrowingShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void NumberOfBooksForBorrowingShouldBeInvalid()
        {
            this.stock.NumberOfBooksForBorrowing = -1;

            Assert.IsFalse(this.stock.NumberOfBooksForBorrowing >= 0);
        }

        /// <summary>
        /// Defines the test method NumberOfBooksForLectureOnlyShouldBeValid.
        /// </summary>
        [TestMethod]
        public void NumberOfBooksForLectureOnlyShouldBeValid()
        {
            Assert.IsTrue(this.stock.NumberOfBooksForLectureOnly > 0);
        }

        /// <summary>
        /// Defines the test method NumberOfBooksForLectureOnlyShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void NumberOfBooksForLectureOnlyShouldBeInvalid()
        {
            this.stock.NumberOfBooksForLectureOnly = 0;

            Assert.IsFalse(this.stock.NumberOfBooksForLectureOnly > 0);
        }

        /// <summary>
        /// Defines the test method InitialStockShouldBeValid.
        /// </summary>
        [TestMethod]
        public void InitialStockShouldBeValid()
        {
            Assert.IsTrue(this.stock.InitialStock > 0);
        }

        /// <summary>
        /// Defines the test method InitialStockShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void InitialStockShouldBeInvalid()
        {
            this.stock.InitialStock = 0;

            Assert.IsFalse(this.stock.InitialStock > 0);
        }

        /// <summary>
        /// Defines the test method EditionShouldBeValid.
        /// </summary>
        [TestMethod]
        public void EditionShouldBeValid()
        {
            Assert.IsNotNull(this.stock.Edition);
        }

        /// <summary>
        /// Defines the test method EditionShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void EditionShouldBeInvalid()
        {
            this.stock.Edition = null;
            Assert.IsNull(this.stock.Edition);
        }
    }
}
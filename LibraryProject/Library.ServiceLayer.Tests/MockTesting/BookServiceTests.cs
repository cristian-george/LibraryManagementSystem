﻿// <copyright file="BookServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.MockTesting
{
    using System.Collections.Generic;
    using Library.DomainLayer.Models;
    using Library.ServiceLayer.Interfaces;
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Defines test class BookServiceTests.
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        /// <summary>
        /// The book service mock.
        /// </summary>
        private Mock<IBookService> bookServiceMock;

        /// <summary>
        /// The book service.
        /// </summary>
        private IBookService bookService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.bookServiceMock = new Mock<IBookService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var book = ProduceModel.GetBookModel();
            _ = this.bookServiceMock.Setup(x => x.Insert(book)).Returns(true);
            this.bookService = this.bookServiceMock.Object;

            var result = this.bookService.Insert(book);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.bookServiceMock.Setup(x => x.Get(null, null))
                .Returns(
                new List<Book>()
                { ProduceModel.GetBookModel() });

            this.bookService = this.bookServiceMock.Object;

            var result = this.bookService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            var book = ProduceModel.GetBookModel();
            book.Id = 1;

            _ = this.bookServiceMock.Setup(x => x.GetById(1))
                .Returns(book);

            this.bookService = this.bookServiceMock.Object;
            var result = this.bookService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            var book = ProduceModel.GetBookModel();

            _ = this.bookServiceMock.Setup(x => x.GetById(1))
                .Returns(book);

            this.bookService = this.bookServiceMock.Object;

            var modifiedBook = this.bookService.GetById(1);
            modifiedBook.Title = "O carte oarecare";

            _ = this.bookServiceMock.Setup(x => x.Update(modifiedBook)).Returns(true);

            var result = this.bookService.Update(modifiedBook);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.bookServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.bookService = this.bookServiceMock.Object;

            var result = this.bookService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
﻿// <copyright file="ReaderServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.Mocks
{
    using System.Collections.Generic;
    using Library.DomainLayer;
    using Library.ServiceLayer.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Defines test class ReaderServiceTests.
    /// </summary>
    [TestClass]
    public class ReaderServiceTests
    {
        /// <summary>
        /// The reader service mock.
        /// </summary>
        private Mock<IUserService> readerServiceMock;

        /// <summary>
        /// The reader service.
        /// </summary>
        private IUserService readerService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.readerServiceMock = new Mock<IUserService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var account = new Account()
            {
                PhoneNumber = "0734525427",
                Email = "gogumortu@gmail.com",
            };

            var reader = new Reader()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.readerServiceMock.Setup(x => x.Insert(reader)).Returns(true);
            this.readerService = this.readerServiceMock.Object;

            var result = this.readerService.Insert(reader);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            var account = new Account()
            {
                PhoneNumber = "0734525427",
                Email = "gogumortu@gmail.com",
            };

            var reader = new Reader()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.readerServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<Reader>()
                { reader });

            this.readerService = this.readerServiceMock.Object;

            var result = this.readerService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            var account = new Account()
            {
                PhoneNumber = "0734525427",
                Email = "gogumortu@gmail.com",
            };

            var reader = new Reader()
            {
                Id = 1,
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };
            _ = this.readerServiceMock.Setup(x => x.GetById(1))
                .Returns(reader);

            this.readerService = this.readerServiceMock.Object;

            var result = this.readerService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Gogu", result.LastName);
            Assert.AreEqual("Mortu", result.FirstName);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            var account = new Account()
            {
                PhoneNumber = "0734525427",
                Email = "gogumortu@gmail.com",
            };

            var reader = new Reader()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.readerServiceMock.Setup(x => x.GetById(1))
                .Returns(reader);

            this.readerService = this.readerServiceMock.Object;

            var modifiedAccount = this.readerService.GetById(1);
            modifiedAccount.LastName = "Alexandru";

            _ = this.readerServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.readerService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.readerServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.readerService = this.readerServiceMock.Object;

            var result = this.readerService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
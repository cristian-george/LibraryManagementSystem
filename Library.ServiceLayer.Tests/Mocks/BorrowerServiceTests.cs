﻿// <copyright file="BorrowerServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Defines test class BorrowerServiceTests.
    /// </summary>
    [TestClass]
    public class BorrowerServiceTests
    {
        /// <summary>
        /// The borrower service mock.
        /// </summary>
        private Mock<IBorrowerService> borrowerServiceMock;

        /// <summary>
        /// The borrower service.
        /// </summary>
        private IBorrowerService borrowerService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.borrowerServiceMock = new Mock<IBorrowerService>();
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

            var borrower = new Borrower()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.borrowerServiceMock.Setup(x => x.Insert(borrower)).Returns(true);
            this.borrowerService = this.borrowerServiceMock.Object;

            var result = this.borrowerService.Insert(borrower);

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

            var borrower = new Borrower()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.borrowerServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<Borrower>()
                { borrower });

            this.borrowerService = this.borrowerServiceMock.Object;

            var result = this.borrowerService.Get();

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

            var borrower = new Borrower()
            {
                Id = 1,
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };
            _ = this.borrowerServiceMock.Setup(x => x.GetById(1))
                .Returns(borrower);

            this.borrowerService = this.borrowerServiceMock.Object;

            var result = this.borrowerService.GetById(1);

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

            var borrower = new Borrower()
            {
                LastName = "Gogu",
                FirstName = "Mortu",
                Address = "Bucuresti, strada Mihai Viteazu, nr 7, bloc C3, ap 26",
                Account = account,
            };

            _ = this.borrowerServiceMock.Setup(x => x.GetById(1))
                .Returns(borrower);

            this.borrowerService = this.borrowerServiceMock.Object;

            var modifiedAccount = this.borrowerService.GetById(1);
            modifiedAccount.LastName = "Alexandru";

            _ = this.borrowerServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.borrowerService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.borrowerServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.borrowerService = this.borrowerServiceMock.Object;

            var result = this.borrowerService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
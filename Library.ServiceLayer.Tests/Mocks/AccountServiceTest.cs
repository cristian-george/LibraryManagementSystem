﻿// <copyright file="AccountServiceTest.cs" company="Transilvania University of Brasov">
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
    /// Defines test class AccountServiceTest.
    /// </summary>
    [TestClass]
    public class AccountServiceTest
    {
        /// <summary>
        /// The account service mock.
        /// </summary>
        private Mock<IAccountService> accountServiceMock;

        /// <summary>
        /// The account service.
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.accountServiceMock = new Mock<IAccountService>();
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
                Email = "validemail@gmail.com",
            };

            _ = this.accountServiceMock.Setup(x => x.Insert(account)).Returns(true);
            this.accountService = this.accountServiceMock.Object;

            var result = this.accountService.Insert(account);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.accountServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<Account>()
                {
                    new ()
                    {
                        PhoneNumber = "0734525427",
                        Email = "validemail@gmail.com",
                    },
                });

            this.accountService = this.accountServiceMock.Object;

            var result = this.accountService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            _ = this.accountServiceMock.Setup(x => x.GetById(1))
                .Returns(new Account
                {
                    Id = 1,
                    PhoneNumber = "0734525427",
                    Email = "validemail@gmail.com",
                });

            this.accountService = this.accountServiceMock.Object;

            var result = this.accountService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("0734525427", result.PhoneNumber);
            Assert.AreEqual("validemail@gmail.com", result.Email);
        }

        /// <summary>
        /// Defines the test method TestUpdate.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            _ = this.accountServiceMock.Setup(x => x.GetById(1))
                .Returns(new Account
                {
                    PhoneNumber = "0734525427",
                    Email = "validemail@gmail.com",
                });

            this.accountService = this.accountServiceMock.Object;

            var modifiedAccount = this.accountService.GetById(1);
            modifiedAccount.Email = "modifiedemail@gmail.ro";

            _ = this.accountServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.accountService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.accountServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.accountService = this.accountServiceMock.Object;

            var result = this.accountService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
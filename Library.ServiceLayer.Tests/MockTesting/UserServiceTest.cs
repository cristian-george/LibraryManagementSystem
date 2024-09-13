// <copyright file="UserServiceTest.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.MockTesting
{
    using System.Collections.Generic;
    using Library.DomainLayer;
    using Library.DomainLayer.Models;
    using Library.ServiceLayer.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Defines test class AccountServiceTest.
    /// </summary>
    [TestClass]
    public class UserServiceTest
    {
        /// <summary>
        /// The account service mock.
        /// </summary>
        private Mock<IUserService> userServiceMock;

        /// <summary>
        /// The account service.
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.userServiceMock = new Mock<IUserService>();
        }

        /// <summary>
        /// Defines the test method TestInsert.
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            var account = new User()
            {
                PhoneNumber = "0734525427",
                Email = "validemail@gmail.com",
            };

            _ = this.userServiceMock.Setup(x => x.Insert(account)).Returns(true);
            this.userService = this.userServiceMock.Object;

            var result = this.userService.Insert(account);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.userServiceMock.Setup(x => x.Get(null, null, null))
                .Returns(
                new List<User>()
                {
                    new ()
                    {
                        PhoneNumber = "0734525427",
                        Email = "validemail@gmail.com",
                    },
                });

            this.userService = this.userServiceMock.Object;

            var result = this.userService.Get();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method TestGetById.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            _ = this.userServiceMock.Setup(x => x.GetById(1))
                .Returns(new User
                {
                    Id = 1,
                    PhoneNumber = "0734525427",
                    Email = "validemail@gmail.com",
                });

            this.userService = this.userServiceMock.Object;

            var result = this.userService.GetById(1);

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
            _ = this.userServiceMock.Setup(x => x.GetById(1))
                .Returns(new User
                {
                    PhoneNumber = "0734525427",
                    Email = "validemail@gmail.com",
                });

            this.userService = this.userServiceMock.Object;

            var modifiedAccount = this.userService.GetById(1);
            modifiedAccount.Email = "modifiedemail@gmail.ro";

            _ = this.userServiceMock.Setup(x => x.Update(modifiedAccount)).Returns(true);

            var result = this.userService.Update(modifiedAccount);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestDelete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            _ = this.userServiceMock.Setup(x => x.DeleteById(1)).Returns(true);
            this.userService = this.userServiceMock.Object;

            var result = this.userService.DeleteById(1);

            Assert.IsTrue(result);
        }
    }
}
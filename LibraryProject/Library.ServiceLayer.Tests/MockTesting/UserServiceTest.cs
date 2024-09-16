// <copyright file="UserServiceTest.cs" company="Transilvania University of Brasov">
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
    /// Defines test class AccountServiceTest.
    /// </summary>
    [TestClass]
    public class UserServiceTest
    {
        /// <summary>
        /// The user service mock.
        /// </summary>
        private Mock<IUserService> userServiceMock;

        /// <summary>
        /// The user service.
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
            var user = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                PhoneNumber = "1234567890",
                Email = "cristian.fieraru@gmail.com",
                UserType = DomainLayer.Enums.EUserType.Reader,
            };

            _ = this.userServiceMock.Setup(x => x.Insert(user)).Returns(true);
            this.userService = this.userServiceMock.Object;

            var result = this.userService.Insert(user);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Defines the test method TestGetAll.
        /// </summary>
        [TestMethod]
        public void TestGetAll()
        {
            _ = this.userServiceMock.Setup(x => x.Get(null, null))
                .Returns(
                new List<User>()
                {
                    new ()
                    {
                        FirstName = "Cristian",
                        LastName = "Fieraru",
                        Address = "str. Strada, nr. 30",
                        PhoneNumber = "1234567890",
                        Email = "cristian.fieraru@gmail.com",
                        UserType = DomainLayer.Enums.EUserType.Reader,
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
                    FirstName = "Cristian",
                    LastName = "Fieraru",
                    Address = "str. Strada, nr. 30",
                    PhoneNumber = "1234567890",
                    Email = "cristian.fieraru@gmail.com",
                    UserType = DomainLayer.Enums.EUserType.Reader,
                });

            this.userService = this.userServiceMock.Object;

            var result = this.userService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("1234567890", result.PhoneNumber);
            Assert.AreEqual("cristian.fieraru@gmail.com", result.Email);
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
                    FirstName = "Cristian",
                    LastName = "Fieraru",
                    Address = "str. Strada, nr. 30",
                    PhoneNumber = "1234567890",
                    Email = "cristian.fieraru@gmail.com",
                    UserType = DomainLayer.Enums.EUserType.Reader,
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
// <copyright file="AccountTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AccountTests.
    /// </summary>
    [TestClass]
    public class AccountTests
    {
        /// <summary>
        /// The account.
        /// </summary>
        private Account account;

        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            this.account = new Account()
            {
                Id = 1,
                PhoneNumber = "0770123456",
                Email = "cristian.fieraru@student.unitbv.ro",
            };
        }

        /// <summary>
        /// Defines the test method AccountPhoneNumberShouldBeWrongIfLengthIsNot10.
        /// </summary>
        [TestMethod]
        public void AccountPhoneNumberShouldBeWrongIfLengthIsNot10()
        {
            this.account.PhoneNumber = "0721";

            Assert.AreNotEqual(10, this.account.PhoneNumber.Length);

            this.account.PhoneNumber = "0770123456789";

            Assert.AreNotEqual(10, this.account.PhoneNumber.Length);
        }

        /// <summary>
        /// Defines the test method AccountPhoneNumberShouldBeNull.
        /// </summary>
        [TestMethod]
        public void AccountPhoneNumberShouldBeNull()
        {
            this.account.PhoneNumber = null;

            Assert.IsNull(this.account.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method AccountIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void AccountIdShouldBeValid()
        {
            Assert.AreEqual(1, this.account.Id);
        }

        /// <summary>
        /// Defines the test method AccountEmailShouldBeValid.
        /// </summary>
        [TestMethod]
        public void AccountEmailShouldBeValid()
        {
            var trimmedEmail = this.account.Email.Trim();

            var addr = new System.Net.Mail.MailAddress(this.account.Email);

            Assert.IsTrue(addr.Address == trimmedEmail);
        }

        /// <summary>
        /// Defines the test method AccountEmailShouldBeInvalidA.
        /// </summary>
        [TestMethod]
        public void AccountEmailShouldBeInvalid()
        {
            this.account.Email = "emailNotValid.";

            Assert.IsFalse(TestUtils.IsEmailValid(this.account.Email));
        }
    }
}
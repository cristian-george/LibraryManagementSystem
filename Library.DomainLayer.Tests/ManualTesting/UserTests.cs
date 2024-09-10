// <copyright file="UserTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Linq;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class UserTests.
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// The user.
        /// </summary>
        private User user;

        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            this.user = new User()
            {
                Id = 1,
                FirstName = "Persoana",
                LastName = "Fizica",
                Address = "Brasov, strada Migdalelor, nr. 86",
                Email = "persoana.fizica@gmail.com",
                PhoneNumber = "0770123456",
                UserType = Enums.EUserType.Reader,
            };
        }

        /// <summary>
        /// Defines the test method IdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void IdShouldBeValid()
        {
            Assert.AreEqual(1, this.user.Id);
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(this.user.FirstName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.user.FirstName = "1223Persoana";
            Assert.IsFalse(this.user.FirstName.All(char.IsLetter));
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            Assert.IsFalse(this.user.LastName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.user.LastName = "Fizica 123";
            Assert.IsTrue(this.user.LastName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method AddressShouldBeValidIfContainsComma.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeValidIfContainsComma()
        {
            Assert.IsTrue(this.user.Address.Contains(','));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfStreetDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfStreetDoesNotExist()
        {
            this.user.Address = "Street Piatra Craiului";
            Assert.IsTrue(this.user.Address.Contains("Street"));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfNumberDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfNumberDoesNotExist()
        {
            bool flag = this.user.Address.Contains("nr.");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfAccountIsNull.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfAccountIsNull()
        {
            this.user.Address = null;
            Assert.IsNull(this.user.Address);
        }

        /// <summary>
        /// Defines the test method PhoneNumberShouldBeWrongIfLengthIsNot10.
        /// </summary>
        [TestMethod]
        public void PhoneNumberShouldBeWrongIfLengthIsNot10()
        {
            this.user.PhoneNumber = "0721";

            Assert.AreNotEqual(10, this.user.PhoneNumber.Length);

            this.user.PhoneNumber = "0770123456789";

            Assert.AreNotEqual(10, this.user.PhoneNumber.Length);
        }

        /// <summary>
        /// Defines the test method PhoneNumberShouldBeNull.
        /// </summary>
        [TestMethod]
        public void PhoneNumberShouldBeNull()
        {
            this.user.PhoneNumber = null;

            Assert.IsNull(this.user.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method EmailShouldBeValid.
        /// </summary>
        [TestMethod]
        public void EmailShouldBeValid()
        {
            var trimmedEmail = this.user.Email.Trim();

            var addr = new System.Net.Mail.MailAddress(this.user.Email);

            Assert.IsTrue(addr.Address == trimmedEmail);
        }

        /// <summary>
        /// Defines the test method EmailShouldBeInvalidA.
        /// </summary>
        [TestMethod]
        public void EmailShouldBeInvalid()
        {
            this.user.Email = "emailNotValid.";

            Assert.IsFalse(this.user.Email.IsEmail());
        }

        /// <summary>
        /// Defines the test method UserShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void UserShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid()
        {
            this.user.Email = "123mail.com";
            this.user.PhoneNumber = "0770456123mmsm";

            bool emailFlag = this.user.Email.IsEmail();
            bool phoneNumberFlag = this.user.PhoneNumber.All(char.IsDigit);

            Assert.AreNotEqual(10, this.user.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method UserShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void UserShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid()
        {
            this.user.PhoneNumber = "0770456123mmsm";

            bool emailFlag = this.user.Email.IsEmail();
            bool phoneNumberFlag = this.user.PhoneNumber.ContainsLetters();

            Assert.AreNotEqual(10, this.user.PhoneNumber.Length);
            Assert.IsTrue(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method UserShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void UserShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid()
        {
            this.user.Email = "123mail.com";

            bool emailFlag = this.user.Email.IsEmail();
            bool phoneNumberFlag = this.user.PhoneNumber.ContainsLetters();

            Assert.AreEqual(10, this.user.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method UserShouldBeValidIfPhoneNumberIsValidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void UserShouldBeValidIfPhoneNumberIsValidAndEmailIsValid()
        {
            bool emailFlag = this.user.Email.IsEmail();
            bool phoneNumberFlag = this.user.PhoneNumber.ContainsLetters();

            Assert.AreEqual(10, this.user.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }
    }
}
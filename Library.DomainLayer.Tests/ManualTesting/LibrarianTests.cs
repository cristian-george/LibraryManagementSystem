// <copyright file="LibrarianTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Linq;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class LibrarianTests.
    /// </summary>
    [TestClass]
    public class LibrarianTests
    {
        /// <summary>
        /// The librarian.
        /// </summary>
        private User librarian;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.librarian = new User()
            {
                Id = 1,
                FirstName = "Biblioteca",
                LastName = "Judeteana",
                Address = "Brasov, Livada Postei, nr. 5",
                Email = "biblioteca@judeteana.ro",
                PhoneNumber = "0770400404",
                UserType = Enums.EUserType.LibrarianReader,
            };
        }

        /// <summary>
        /// Defines the test method LibrarianIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LibrarianIdShouldBeValid()
        {
            Assert.AreEqual(1, this.librarian.Id);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            Assert.IsFalse(this.librarian.LastName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.librarian.LastName = "Judeteana 123";
            Assert.IsTrue(this.librarian.LastName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(this.librarian.FirstName.ContainsDigits());
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.librarian.FirstName = "1223Biblioteca";
            Assert.IsFalse(this.librarian.FirstName.All(char.IsLetter));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeValidIfContainsComma.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeValidIfContainsComma()
        {
            Assert.IsTrue(this.librarian.Address.Contains(','));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfStreetDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfStreetDoesNotExist()
        {
            this.librarian.Address = "Street Piatra Craiului";
            Assert.IsTrue(this.librarian.Address.Contains("Street"));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfNumberDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfNumberDoesNotExist()
        {
            bool flag = this.librarian.Address.Contains("nr.");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid()
        {
            this.librarian.Email = "123mail.com";
            this.librarian.PhoneNumber = "0770456123mmsm";

            bool emailFlag = this.librarian.Email.IsEmail();
            bool phoneNumberFlag = this.librarian.PhoneNumber.All(char.IsDigit);

            Assert.AreNotEqual(10, this.librarian.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid()
        {
            this.librarian.PhoneNumber = "0770456123mmsm";

            bool emailFlag = this.librarian.Email.IsEmail();
            bool phoneNumberFlag = this.librarian.PhoneNumber.ContainsLetters();

            Assert.AreNotEqual(10, this.librarian.PhoneNumber.Length);
            Assert.IsTrue(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid()
        {
            this.librarian.Email = "123mail.com";

            bool emailFlag = this.librarian.Email.IsEmail();
            bool phoneNumberFlag = this.librarian.PhoneNumber.ContainsLetters();

            Assert.AreEqual(10, this.librarian.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid()
        {
            bool emailFlag = this.librarian.Email.IsEmail();
            bool phoneNumberFlag = this.librarian.PhoneNumber.ContainsLetters();

            Assert.AreEqual(10, this.librarian.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianShouldBeReader.
        /// </summary>
        [TestMethod]
        public void LibrarianShouldBeReader()
        {
            Assert.IsTrue(this.librarian.UserType == Enums.EUserType.LibrarianReader);
        }

        /// <summary>
        /// Defines the test method LibrarianShouldNotBeReader.
        /// </summary>
        [TestMethod]
        public void LibrarianShouldNotBeReader()
        {
            this.librarian.UserType = Enums.EUserType.Librarian;
            Assert.IsFalse(this.librarian.UserType == Enums.EUserType.LibrarianReader);
        }
    }
}
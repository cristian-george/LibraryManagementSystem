// <copyright file="LibrarianTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System.Linq;
    using Library.DomainLayer;
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
        private Librarian librarian;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var account = new Account()
            {
                Id = 1,
                Email = "biblioteca@judeteana.ro",
                PhoneNumber = "0770400404",
            };

            this.librarian = new Librarian()
            {
                Id = 1,
                FirstName = "Biblioteca",
                LastName = "Judeteana",
                Address = "Brasov, Livada Postei, nr. 5",
                IsReader = true,
                Account = account,
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
            Assert.IsFalse(TestUtils.ContainsDigits(this.librarian.LastName));
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.librarian.LastName = "Judeteana 123";
            Assert.IsTrue(TestUtils.ContainsDigits(this.librarian.LastName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.librarian.FirstName));
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
        /// Defines the test method AddressShouldBeInvalidIfAccountIsNull.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfAccountIsNull()
        {
            this.librarian.Account = null;
            Assert.IsNull(this.librarian.Account);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid()
        {
            this.librarian.Account.Email = "123mail.com";
            this.librarian.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.librarian.Account.Email);
            bool phoneNumberFlag = this.librarian.Account.PhoneNumber.All(char.IsDigit);

            Assert.AreNotEqual(10, this.librarian.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid()
        {
            this.librarian.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.librarian.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.librarian.Account.PhoneNumber);

            Assert.AreNotEqual(10, this.librarian.Account.PhoneNumber.Length);
            Assert.IsTrue(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid()
        {
            this.librarian.Account.Email = "123mail.com";

            bool emailFlag = TestUtils.IsEmailValid(this.librarian.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.librarian.Account.PhoneNumber);

            Assert.AreEqual(10, this.librarian.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void LibrarianAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid()
        {
            bool emailFlag = TestUtils.IsEmailValid(this.librarian.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.librarian.Account.PhoneNumber);

            Assert.AreEqual(10, this.librarian.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method LibrarianShouldBeReader.
        /// </summary>
        [TestMethod]
        public void LibrarianShouldBeReader()
        {
            Assert.IsTrue((bool)this.librarian.IsReader);
        }

        /// <summary>
        /// Defines the test method LibrarianShouldNotBeReader.
        /// </summary>
        [TestMethod]
        public void LibrarianShouldNotBeReader()
        {
            this.librarian.IsReader = false;
            Assert.IsFalse((bool)this.librarian.IsReader);
        }
    }
}
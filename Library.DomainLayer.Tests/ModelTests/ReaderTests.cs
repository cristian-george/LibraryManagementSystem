// <copyright file="ReaderTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System.Linq;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class ReaderTests.
    /// </summary>
    [TestClass]
    public class ReaderTests
    {
        /// <summary>
        /// The reader.
        /// </summary>
        private Reader reader;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var account = new Account()
            {
                Id = 1,
                Email = "doresc@imprumut.ro",
                PhoneNumber = "0770400404",
            };

            this.reader = new Reader()
            {
                Id = 1,
                FirstName = "Persoana",
                LastName = "Fizica",
                Address = "Brasov, strada Migdalelor, nr. 86",
                Account = account,
            };
        }

        /// <summary>
        /// Defines the test method ReaderIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void ReaderIdShouldBeValid()
        {
            Assert.AreEqual(1, this.reader.Id);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.reader.LastName));
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.reader.LastName = "Fizica 123";
            Assert.IsTrue(TestUtils.ContainsDigits(this.reader.LastName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.reader.FirstName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.reader.FirstName = "1223Persoana";
            Assert.IsFalse(this.reader.FirstName.All(char.IsLetter));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeValidIfContainsComma.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeValidIfContainsComma()
        {
            Assert.IsTrue(this.reader.Address.Contains(','));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfStreetDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfStreetDoesNotExist()
        {
            this.reader.Address = "Street Piatra Craiului";
            Assert.IsTrue(this.reader.Address.Contains("Street"));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfNumberDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfNumberDoesNotExist()
        {
            bool flag = this.reader.Address.Contains("nr.");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfAccountIsNull.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfAccountIsNull()
        {
            this.reader.Account = null;
            Assert.IsNull(this.reader.Account);
        }

        /// <summary>
        /// Defines the test method ReaderAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void ReaderAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid()
        {
            this.reader.Account.Email = "123mail.com";
            this.reader.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.reader.Account.Email);
            bool phoneNumberFlag = this.reader.Account.PhoneNumber.All(char.IsDigit);

            Assert.AreNotEqual(10, this.reader.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method ReaderAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void ReaderAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid()
        {
            this.reader.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.reader.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.reader.Account.PhoneNumber);

            Assert.AreNotEqual(10, this.reader.Account.PhoneNumber.Length);
            Assert.IsTrue(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method ReaderAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void ReaderAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid()
        {
            this.reader.Account.Email = "123mail.com";

            bool emailFlag = TestUtils.IsEmailValid(this.reader.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.reader.Account.PhoneNumber);

            Assert.AreEqual(10, this.reader.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method ReaderAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void ReaderAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid()
        {
            bool emailFlag = TestUtils.IsEmailValid(this.reader.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.reader.Account.PhoneNumber);

            Assert.AreEqual(10, this.reader.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }
    }
}
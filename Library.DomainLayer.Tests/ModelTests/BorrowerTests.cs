// <copyright file="BorrowerTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System.Linq;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BorrowerTests.
    /// </summary>
    [TestClass]
    public class BorrowerTests
    {
        /// <summary>
        /// The borrower.
        /// </summary>
        private Borrower borrower;

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

            this.borrower = new Borrower()
            {
                Id = 1,
                FirstName = "Persoana",
                LastName = "Fizica",
                Address = "Brasov, strada Migdalelor, nr. 86",
                Account = account,
            };
        }

        /// <summary>
        /// Defines the test method BorrowerIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void BorrowerIdShouldBeValid()
        {
            Assert.AreEqual(1, this.borrower.Id);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.borrower.LastName));
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.borrower.LastName = "Fizica 123";
            Assert.IsTrue(TestUtils.ContainsDigits(this.borrower.LastName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            Assert.IsFalse(TestUtils.ContainsDigits(this.borrower.FirstName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.borrower.FirstName = "1223Persoana";
            Assert.IsFalse(this.borrower.FirstName.All(char.IsLetter));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeValidIfContainsComma.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeValidIfContainsComma()
        {
            Assert.IsTrue(this.borrower.Address.Contains(','));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfStreetDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfStreetDoesNotExist()
        {
            this.borrower.Address = "Street Piatra Craiului";
            Assert.IsTrue(this.borrower.Address.Contains("Street"));
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfNumberDoesNotExist.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfNumberDoesNotExist()
        {
            bool flag = this.borrower.Address.Contains("nr.");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Defines the test method AddressShouldBeInvalidIfAccountIsNull.
        /// </summary>
        [TestMethod]
        public void AddressShouldBeInvalidIfAccountIsNull()
        {
            this.borrower.Account = null;
            Assert.IsNull(this.borrower.Account);
        }

        /// <summary>
        /// Defines the test method BorrowerAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void BorrowerAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsInvalid()
        {
            this.borrower.Account.Email = "123mail.com";
            this.borrower.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.borrower.Account.Email);
            bool phoneNumberFlag = this.borrower.Account.PhoneNumber.All(char.IsDigit);

            Assert.AreNotEqual(10, this.borrower.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method BorrowerAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void BorrowerAccountShouldBeInvalidIfPhoneNumberIsInvalidAndEmailIsValid()
        {
            this.borrower.Account.PhoneNumber = "0770456123mmsm";

            bool emailFlag = TestUtils.IsEmailValid(this.borrower.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.borrower.Account.PhoneNumber);

            Assert.AreNotEqual(10, this.borrower.Account.PhoneNumber.Length);
            Assert.IsTrue(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }

        /// <summary>
        /// Defines the test method BorrowerAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid.
        /// </summary>
        [TestMethod]
        public void BorrowerAccountShouldBeInvalidIfPhoneNumberIsValidAndEmailIsInvalid()
        {
            this.borrower.Account.Email = "123mail.com";

            bool emailFlag = TestUtils.IsEmailValid(this.borrower.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.borrower.Account.PhoneNumber);

            Assert.AreEqual(10, this.borrower.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsFalse(emailFlag);
        }

        /// <summary>
        /// Defines the test method BorrowerAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid.
        /// </summary>
        [TestMethod]
        public void BorrowerAccountShouldBeValidIfPhoneNumberIsValidAndEmailIsValid()
        {
            bool emailFlag = TestUtils.IsEmailValid(this.borrower.Account.Email);
            bool phoneNumberFlag = TestUtils.ContainsLetters(this.borrower.Account.PhoneNumber);

            Assert.AreEqual(10, this.borrower.Account.PhoneNumber.Length);
            Assert.IsFalse(phoneNumberFlag);
            Assert.IsTrue(emailFlag);
        }
    }
}
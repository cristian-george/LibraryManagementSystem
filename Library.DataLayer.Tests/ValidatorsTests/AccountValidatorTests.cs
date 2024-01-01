// <copyright file="AccountValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Tests.ValidatorsTests
{
    using FluentValidation.TestHelper;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AccountValidatorTests.
    /// </summary>
    [TestClass]
    public class AccountValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private AccountValidator validator;

        /// <summary>
        /// The entity.
        /// </summary>
        private Account account;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new ();
            this.account = new ()
            {
                Email = "validemail@mail.com",
                PhoneNumber = "0770123456",
            };
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberIsNull()
        {
            this.account.PhoneNumber = null;

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPhoneNumberIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPhoneNumberIsNotNull()
        {
            var result = this.validator.TestValidate(this.account);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberIsEmpty()
        {
            this.account.PhoneNumber = string.Empty;

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPhoneNumberIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPhoneNumberIsNotEmpty()
        {
            var result = this.validator.TestValidate(this.account);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberHasMoreThan10Digits.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberHasMoreThan10Digits()
        {
            this.account.PhoneNumber = "0770123456789";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberHasLessThan10Digits.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberHasLessThan10Digits()
        {
            this.account.PhoneNumber = "0770456";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberContainsLetters.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberContainsLetters()
        {
            this.account.PhoneNumber = "telefon0770";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPhoneNumberDoesNotContainLetters.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPhoneNumberDoesNotContainLetters()
        {
            var result = this.validator.TestValidate(this.account);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPhoneNumberIsValid.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPhoneNumberIsValid()
        {
            var result = this.validator.TestValidate(this.account);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters1()
        {
            this.account.Email = "invalidemail";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters2.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters2()
        {
            this.account.Email = "invalidemail@";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters3.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters3()
        {
            this.account.Email = "invalidemail.mail";

            var result = this.validator.TestValidate(this.account);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenEmailIsValid.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenEmailIsValid()
        {
            var result = this.validator.TestValidate(this.account);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
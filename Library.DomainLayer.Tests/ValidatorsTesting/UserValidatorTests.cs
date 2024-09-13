// <copyright file="UserValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class UserValidatorTests.
    /// </summary>
    [TestClass]
    public class UserValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private UserValidator validator;

        /// <summary>
        /// The entity.
        /// </summary>
        private User user;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new UserValidator();
            this.user = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                Email = "cristian.fieraru@unitbv.ro",
                PhoneNumber = "1234567890",
                UserType = Enums.EUserType.Reader,
            };
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveAnyErrors()
        {
            var result = this.validator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            this.user.FirstName = null;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            this.user.FirstName = string.Empty;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameLengthIsLessThanOne()
        {
            this.user.FirstName = "C";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNotAValidName()
        {
            this.user.FirstName = "---Cristian000";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            this.user.LastName = null;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            this.user.LastName = string.Empty;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameLengthIsLessThanOne()
        {
            this.user.LastName = "F";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNotAValidName()
        {
            this.user.LastName = "---Fieraru000";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressIsNull()
        {
            this.user.Address = null;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressIsEmpty()
        {
            this.user.Address = string.Empty;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressLengthIsLessThanOne()
        {
            this.user.Address = "S";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberIsNull()
        {
            this.user.PhoneNumber = null;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberIsEmpty()
        {
            this.user.PhoneNumber = string.Empty;

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberHasMoreThan10Digits.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberHasMoreThan10Digits()
        {
            this.user.PhoneNumber = "0770123456789";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberHasLessThan10Digits.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberHasLessThan10Digits()
        {
            this.user.PhoneNumber = "0770456";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPhoneNumberContainsLetters.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPhoneNumberContainsLetters()
        {
            this.user.PhoneNumber = "telefon0770";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.PhoneNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters1()
        {
            this.user.Email = "invalidemail";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters2.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters2()
        {
            this.user.Email = "invalidemail@";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEmailDoesNotContainProperCharacters3.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEmailDoesNotContainProperCharacters3()
        {
            this.user.Email = "invalidemail.mail";

            var result = this.validator.TestValidate(this.user);
            _ = result.ShouldHaveValidationErrorFor(a => a.Email);
        }
    }
}

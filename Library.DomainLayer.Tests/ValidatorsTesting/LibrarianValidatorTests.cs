// <copyright file="LibrarianValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class LibrarianValidatorTests.
    /// </summary>
    [TestClass]
    public class LibrarianValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private UserValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new ();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            var model = new User()
            {
                FirstName = null,
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsNotNull()
        {
            var model = new User()
            {
                FirstName = "ceva",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            var model = new User()
            {
                FirstName = string.Empty,
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsNotEmpty()
        {
            var model = new User()
            {
                FirstName = "altceva",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameLengthIsLessThanOne()
        {
            var model = new User()
            {
                FirstName = "q",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsHigherThanOne()
        {
            var model = new User()
            {
                FirstName = "qrwer",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNotAValidName()
        {
            var model = new User()
            {
                FirstName = "--gds031",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsAValidName()
        {
            var model = new User()
            {
                FirstName = "Fieraru",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            var model = new User()
            {
                LastName = null,
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsNotNull()
        {
            var model = new User()
            {
                LastName = "ceva",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            var model = new User()
            {
                LastName = string.Empty,
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsNotEmpty()
        {
            var model = new User()
            {
                LastName = "altceva",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameLengthIsLessThanOne()
        {
            var model = new User()
            {
                LastName = "q",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsHigherThanOne()
        {
            var model = new User()
            {
                LastName = "qrwer",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNotAValidName()
        {
            var model = new User()
            {
                LastName = "--gds031",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsAValidName()
        {
            var model = new User()
            {
                LastName = "Fieraru",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressIsNull()
        {
            var model = new User()
            {
                Address = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenAddressIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenAddressIsNotNull()
        {
            var model = new User()
            {
                Address = "ceva",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressIsEmpty()
        {
            var model = new User()
            {
                Address = string.Empty,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenAddressIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenAddressIsNotEmpty()
        {
            var model = new User()
            {
                Address = "altceva",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAddressLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAddressLengthIsLessThanOne()
        {
            var model = new User()
            {
                Address = "q",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Address);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenAddressIsHigherThanTwo.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenAddressIsHigherThanTwo()
        {
            var model = new User()
            {
                Address = "qrwer",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Address);
        }
    }
}
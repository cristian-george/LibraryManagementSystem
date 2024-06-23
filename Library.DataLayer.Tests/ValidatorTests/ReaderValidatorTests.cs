// <copyright file="ReaderValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Tests.ValidatorTests
{
    using FluentValidation.TestHelper;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class ReaderValidatorTests.
    /// </summary>
    [TestClass]
    public class ReaderValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private ReaderValidator validator;

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
            var model = new Reader()
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
            var model = new Reader()
            {
                FirstName = "Cristian",
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
            var model = new Reader()
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
            var model = new Reader()
            {
                FirstName = "Cristian",
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
            {
                LastName = "Cristian",
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
            var model = new Reader()
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
            var model = new Reader()
            {
                LastName = "Cristian",
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
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
            var model = new Reader()
            {
                Address = "adresa",
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
            var model = new Reader()
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
            var model = new Reader()
            {
                Address = "adresa",
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
            var model = new Reader()
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
            var model = new Reader()
            {
                Address = "qrwer",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Address);
        }
    }
}
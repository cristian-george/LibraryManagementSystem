// <copyright file="AuthorValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Tests.ValidatorsTests
{
    using FluentValidation.TestHelper;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AuthorValidatorTests.
    /// </summary>
    [TestClass]
    public class AuthorValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private AuthorValidator validator;

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
            var model = new Author()
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
            var model = new Author()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            var model = new Author()
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
            var model = new Author()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameLenghtIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameLenghtIsLessThanOne()
        {
            var model = new Author()
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
            var model = new Author()
            {
                FirstName = "qrwer",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNotAValidName()
        {
            var model = new Author()
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
            var model = new Author()
            {
                FirstName = "Fieraru",
                LastName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            var model = new Author()
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
            var model = new Author()
            {
                LastName = "Cristian",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            var model = new Author()
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
            var model = new Author()
            {
                LastName = "Cristian",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameLenghtIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameLenghtIsLessThanOne()
        {
            var model = new Author()
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
            var model = new Author()
            {
                LastName = "qrwer",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNotAValidName()
        {
            var model = new Author()
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
            var model = new Author()
            {
                LastName = "Fieraru",
                FirstName = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
// <copyright file="BorrowerValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Tests.ValidatorTests
{
    using FluentValidation.TestHelper;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BorrowerValidatorTests.
    /// </summary>
    [TestClass]
    public class BorrowerValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private BorrowerValidator validator;

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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
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
            var model = new Borrower()
            {
                Address = "qrwer",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Address);
        }
    }
}
// <copyright file="EditionValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class EditionValidatorTests.
    /// </summary>
    [TestClass]
    public class EditionValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private EditionValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new EditionValidator();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPublisherIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPublisherIsNull()
        {
            var model = new Edition()
            {
                Publisher = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPublisherIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPublisherIsNotNull()
        {
            var model = new Edition()
            {
                Publisher = "NotNull",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPublisherIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPublisherIsEmpty()
        {
            var model = new Edition()
            {
                Publisher = string.Empty,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPublisherIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPublisherIsNotEmpty()
        {
            var model = new Edition()
            {
                Publisher = "notEmpty",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPublisherLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPublisherLengthIsLessThanOne()
        {
            var model = new Edition()
            {
                Publisher = "q",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPublisherIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPublisherIsHigherThanOne()
        {
            var model = new Edition()
            {
                Publisher = "qwert",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Publisher);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenYearIsLessThan1850.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenYearIsLessThan1850()
        {
            var model = new Edition()
            {
                Year = 1820,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Year);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenYearIsGreaterThan1850.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenYearIsGreaterThan1850()
        {
            var model = new Edition()
            {
                Year = 1870,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Year);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenYearIsLessThan2024.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenYearIsGreaterThan2024()
        {
            var model = new Edition()
            {
                Year = 2040,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(a => a.Year);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenYearIsLessThan2024.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenYearIsLessThan2024()
        {
            var model = new Edition()
            {
                Year = 2020,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Year);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEditionNumberIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEditionNumberIsNotGreaterThan1()
        {
            var model = new Edition()
            {
                EditionNumber = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.EditionNumber);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenEditionNumberIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenEditionNumberIsGreaterThan1()
        {
            var model = new Edition()
            {
                EditionNumber = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.EditionNumber);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumberOfPagesIsNotGreaterThan10.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumberOfPagesIsNotGreaterThan10()
        {
            var model = new Edition()
            {
                NumberOfPages = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.NumberOfPages);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNumberOfPagesIsGreaterThan10.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNumberOfPagesIsGreaterThan10()
        {
            var model = new Edition()
            {
                NumberOfPages = 20,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.NumberOfPages);
        }
    }
}
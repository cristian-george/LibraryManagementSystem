// <copyright file="BookValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BookValidatorTests.
    /// </summary>
    [TestClass]
    public class BookValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private BookValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenTitleIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenTitleIsNull()
        {
            var model = new Book()
            {
                Title = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenTitleIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenTitleIsNotNull()
        {
            var model = new Book()
            {
                Title = "Dincolo de stele",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenTitleIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenTitleIsEmpty()
        {
            var model = new Book()
            {
                Title = string.Empty,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenTitleIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenTitleIsNotEmpty()
        {
            var model = new Book()
            {
                Title = "Dincolo de stele",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenTitleLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenTitleLengthIsLessThanOne()
        {
            var model = new Book()
            {
                Title = "q",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenTitleIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenTitleIsHigherThanOne()
        {
            var model = new Book()
            {
                Title = "qrwer",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenTitleIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenTitleIsNotAValidName()
        {
            var model = new Book()
            {
                Title = "--gds031",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenTitleIsAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenTitleIsAValidName()
        {
            var model = new Book()
            {
                Title = "Fieraru",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Title);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAuthorCollectionIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAuthorCollectionIsNull()
        {
            var model = new Book()
            {
                Authors = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Authors);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAuthorCollectionIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenAuthorCollectionIsEmpty()
        {
            var model = new Book()
            {
                Authors = new List<Author>(),
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Authors);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenDomainsCollectionIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenDomainsCollectionIsNull()
        {
            var model = new Book()
            {
                Domains = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Domains);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenDomainsCollectionIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenDomainsCollectionIsEmpty()
        {
            var model = new Book()
            {
                Domains = new List<Domain>(),
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Domains);
        }
    }
}
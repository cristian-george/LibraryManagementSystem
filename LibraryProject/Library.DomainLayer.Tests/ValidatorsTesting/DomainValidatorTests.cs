﻿// <copyright file="DomainValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class DomainValidatorTests.
    /// </summary>
    [TestClass]
    public class DomainValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private DomainValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new ();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNameIsNull()
        {
            var model = new Domain()
            {
                Name = null,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNameIsNotNull()
        {
            var model = new Domain()
            {
                Name = "ceva",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Name);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNameIsEmpty()
        {
            var model = new Domain()
            {
                Name = string.Empty,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNameIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNameIsNotEmpty()
        {
            var model = new Domain()
            {
                Name = "altceva",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Name);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNameLengthIsLessThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNameLengthIsLessThanOne()
        {
            var model = new Domain()
            {
                Name = "q",
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNameIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNameIsHigherThanOne()
        {
            var model = new Domain()
            {
                Name = "qrwer",
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Name);
        }
    }
}
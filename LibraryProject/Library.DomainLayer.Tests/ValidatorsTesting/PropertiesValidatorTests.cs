// <copyright file="PropertiesValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class PropertiesValidatorTests.
    /// </summary>
    [TestClass]
    public class PropertiesValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private PropertiesValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new PropertiesValidator();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenDomeniiIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenDomeniiIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Domenii = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Domenii);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenDomeniiIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenDomeniiIsGreaterThan1()
        {
            var model = new Properties()
            {
                Domenii = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Domenii);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNrMaximCartiImprumutateIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNrMaximCartiImprumutateIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                C = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.C);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNrMaximCartiImprumutateIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNrMaximCartiImprumutateIsGreaterThan1()
        {
            var model = new Properties()
            {
                C = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.C);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPerioadaIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPerioadaIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Per = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Per);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPerioadaIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPerioadaIsGreaterThan1()
        {
            var model = new Properties()
            {
                Per = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Per);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNrMaximCartiImprumutateAcelasiDomeniuIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNrMaximCartiImprumutateAcelasiDomeniuIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                D = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.D);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNrMaximCartiImprumutateAcelasiDomeniuIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNrMaximCartiImprumutateAcelasiDomeniuIsGreaterThan1()
        {
            var model = new Properties()
            {
                D = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.D);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumarMaximCartiIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumarMaximCartiIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Nmc = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Nmc);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNumarMaximCartiIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNumarMaximCartiIsGreaterThan1()
        {
            var model = new Properties()
            {
                Nmc = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Nmc);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLimitaMaximaImprumutIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLimitaMaximaImprumutIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Lim = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Lim);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLimitaMaximaImprumutIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLimitaMaximaImprumutIsGreaterThan1()
        {
            var model = new Properties()
            {
                Lim = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Lim);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenDeltaIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenDeltaIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Delta = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Delta);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenDeltaIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenDeltaIsGreaterThan1()
        {
            var model = new Properties()
            {
                Delta = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Delta);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumarCartiImprumutateZilnicIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumarCartiImprumutateZilnicIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Ncz = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Ncz);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenNumarCartiImprumutateZilnicIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenNumarCartiImprumutateZilnicIsGreaterThan1()
        {
            var model = new Properties()
            {
                Ncz = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Ncz);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenPersimpIsNotGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenPersimpIsNotGreaterThan1()
        {
            var model = new Properties()
            {
                Persimp = 0,
            };

            var result = this.validator.TestValidate(model);
            _ = result.ShouldHaveValidationErrorFor(a => a.Persimp);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenPersimpIsGreaterThan1.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenPersimpIsGreaterThan1()
        {
            var model = new Properties()
            {
                Persimp = 2,
            };

            var result = this.validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(a => a.Persimp);
        }
    }
}
// <copyright file="StockValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AccountValidatorTests.
    /// </summary>
    [TestClass]
    public class StockValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private StockValidator validator;

        /// <summary>
        /// The entity.
        /// </summary>
        private Stock stock;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new StockValidator();
            this.stock = ProduceModel.GetStockModelWithPaperback();
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveAnyErrors.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveAnyErrors()
        {
            var result = this.validator.TestValidate(this.stock);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenEditionIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenEditionIsNull()
        {
            this.stock.Edition = null;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.Edition);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumberOfBooksForBorrowingIsNegative.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumberOfBooksForBorrowingIsNegative()
        {
            this.stock.NumberOfBooksForBorrowing = -1;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.NumberOfBooksForBorrowing);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumberOfBooksForLectureOnlyIsNegative.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumberOfBooksForLectureOnlyIsNegative()
        {
            this.stock.NumberOfBooksForLectureOnly = -1;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.NumberOfBooksForLectureOnly);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenNumberOfBooksForLectureOnlyIsZero.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenNumberOfBooksForLectureOnlyIsZero()
        {
            this.stock.NumberOfBooksForLectureOnly = 0;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.NumberOfBooksForLectureOnly);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenInitialStockIsNegative.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenInitialStockIsNegative()
        {
            this.stock.InitialStock = -1;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.InitialStock);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenInitialStockIsZero.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenInitialStockIsZero()
        {
            this.stock.InitialStock = 0;

            var result = this.validator.TestValidate(this.stock);
            _ = result.ShouldHaveValidationErrorFor(a => a.InitialStock);
        }
    }
}
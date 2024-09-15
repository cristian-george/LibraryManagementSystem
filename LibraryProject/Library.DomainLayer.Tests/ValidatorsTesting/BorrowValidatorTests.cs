// <copyright file="BorrowValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using System;
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class BorrowValidatorTests.
    /// </summary>
    [TestClass]
    public class BorrowValidatorTests
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private BorrowValidator validator;

        private Borrow borrow;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new BorrowValidator();

            this.borrow = ProduceModel.GetBorrowModelWithOneStock1();
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenBorrowDateIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenBorrowDateIsNotNull()
        {
            this.borrow.BorrowDate = DateTime.Now.AddMonths(-1);

            var result = this.validator.TestValidate(this.borrow);
            result.ShouldNotHaveValidationErrorFor(a => a.BorrowDate);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenEndDateIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenEndDateIsNotNull()
        {
            this.borrow.ReturnDate = DateTime.Now;

            var result = this.validator.TestValidate(this.borrow);
            result.ShouldNotHaveValidationErrorFor(a => a.ReturnDate);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenBorrowedBooksCollectionIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenBorrowedBooksCollectionIsNull()
        {
            this.borrow.Stocks = null;

            var result = this.validator.TestValidate(this.borrow);
            _ = result.ShouldHaveValidationErrorFor(a => a.Stocks);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenBorrowDateIsHigherThanDateTimeNow.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenBorrowDateIsHigherThanDateTimeNow()
        {
            this.borrow.BorrowDate = DateTime.Now.AddMonths(1);

            var result = this.validator.TestValidate(this.borrow);
            _ = result.ShouldHaveValidationErrorFor(a => a.BorrowDate);
        }
    }
}
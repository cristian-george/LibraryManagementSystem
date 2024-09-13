// <copyright file="BorrowValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using System;
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new ();
        }

        ///// <summary>
        ///// Defines the test method ShouldHaveErrorWhenBorrowDateIsNull.
        ///// </summary>
        //[TestMethod]
        //public void ShouldHaveErrorWhenBorrowDateIsNull()
        //{
        //    var model = new Borrow()
        //    {
        //        BorrowDate = null,
        //    };

        //    var result = this.validator.TestValidate(model);
        //    _ = result.ShouldHaveValidationErrorFor(a => a.BorrowDate);
        //}

        ///// <summary>
        ///// Defines the test method ShouldNotHaveErrorWhenBorrowDateIsNotNull.
        ///// </summary>
        //[TestMethod]
        //public void ShouldNotHaveErrorWhenBorrowDateIsNotNull()
        //{
        //    var model = new Borrow()
        //    {
        //        BorrowDate = DateTime.Now.AddMonths(-1),
        //    };

        //    var result = this.validator.TestValidate(model);
        //    result.ShouldNotHaveValidationErrorFor(a => a.BorrowDate);
        //}

        ///// <summary>
        ///// Defines the test method ShouldHaveErrorWhenEndDateIsNull.
        ///// </summary>
        //[TestMethod]
        //public void ShouldHaveErrorWhenEndDateIsNull()
        //{
        //    var model = new Borrow()
        //    {
        //        ReturnDate = null,
        //    };

        //    var result = this.validator.TestValidate(model);
        //    _ = result.ShouldHaveValidationErrorFor(a => a.ReturnDate);
        //}

        ///// <summary>
        ///// Defines the test method ShouldNotHaveErrorWhenEndDateIsNotNull.
        ///// </summary>
        //[TestMethod]
        //public void ShouldNotHaveErrorWhenEndDateIsNotNull()
        //{
        //    var model = new Borrow()
        //    {
        //        ReturnDate = DateTime.Now,
        //    };

        //    var result = this.validator.TestValidate(model);
        //    result.ShouldNotHaveValidationErrorFor(a => a.ReturnDate);
        //}

        ///// <summary>
        ///// Defines the test method ShouldHaveErrorWhenBorrowedBooksCollectionIsNull.
        ///// </summary>
        //[TestMethod]
        //public void ShouldHaveErrorWhenBorrowedBooksCollectionIsNull()
        //{
        //    var model = new Borrow()
        //    {
        //        Stocks = null,
        //    };

        //    var result = this.validator.TestValidate(model);
        //    _ = result.ShouldHaveValidationErrorFor(a => a.Stocks);
        //}

        ///// <summary>
        ///// Defines the test method ShouldHaveErrorWhenBorrowDateIsHighenThanDateTimeNow.
        ///// </summary>
        //[TestMethod]
        //public void ShouldHaveErrorWhenBorrowDateIsHighenThanDateTimeNow()
        //{
        //    var model = new Borrow()
        //    {
        //        BorrowDate = DateTime.Now.AddMonths(1),
        //    };

        //    var result = this.validator.TestValidate(model);
        //    _ = result.ShouldHaveValidationErrorFor(a => a.BorrowDate);
        //}
    }
}
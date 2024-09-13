// <copyright file="StockValidatorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ValidatorsTesting
{
    using FluentValidation.TestHelper;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
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
        }
    }
}
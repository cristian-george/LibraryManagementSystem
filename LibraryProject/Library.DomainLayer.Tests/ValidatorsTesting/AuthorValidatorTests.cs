﻿// <copyright file="AuthorValidatorTests.cs" company="Transilvania University of Brasov">
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
        /// The entity.
        /// </summary>
        private Author author;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.validator = new AuthorValidator();
            this.author = new Author()
            {
                FirstName = "Mihail",
                LastName = "Sadoveanu",
                Books = new List<Book>()
                {
                    new Book()
                    {
                        Title = "Hanu Ancutei",
                        Genre = "Carte de povestiri",
                        Domains = new List<Domain>()
                        {
                            new ()
                            {
                                Name = "Literatura",
                            },
                        },
                    },
                },
            };
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            this.author.FirstName = null;

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsNotNull()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            this.author.FirstName = string.Empty;

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsNotEmpty()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameLengthIsLessThanTwo.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameLengthIsLessThanTwo()
        {
            this.author.FirstName = "M";

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameLengthIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameLengthIsHigherThanOne()
        {
            this.author.FirstName = "Mi";

            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenFirstNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenFirstNameIsNotAValidName()
        {
            this.author.FirstName = "---Mihail123";

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.FirstName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenFirstNameIsAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenFirstNameIsAValidName()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            this.author.LastName = null;

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsNotNull.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsNotNull()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            this.author.LastName = string.Empty;

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsNotEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsNotEmpty()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameLengthIsLessThanTwo.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameLengthIsLessThanTwo()
        {
            this.author.LastName = "S";

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameLengthIsHigherThanOne.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameLengthIsHigherThanOne()
        {
            this.author.LastName = "Sa";

            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenLastNameIsNotAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenLastNameIsNotAValidName()
        {
            this.author.LastName = "--Sadoveanu123";

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.LastName);
        }

        /// <summary>
        /// Defines the test method ShouldNotHaveErrorWhenLastNameIsAValidName.
        /// </summary>
        [TestMethod]
        public void ShouldNotHaveErrorWhenLastNameIsAValidName()
        {
            var result = this.validator.TestValidate(this.author);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenBooksCollectionIsNull.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenBooksCollectionIsNull()
        {
            this.author.Books = null;

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.Books);
        }

        /// <summary>
        /// Defines the test method ShouldHaveErrorWhenAuthorCollectionIsEmpty.
        /// </summary>
        [TestMethod]
        public void ShouldHaveErrorWhenBooksCollectionIsEmpty()
        {
            this.author.Books.Clear();

            var result = this.validator.TestValidate(this.author);
            _ = result.ShouldHaveValidationErrorFor(a => a.Books);
        }
    }
}
// <copyright file="AuthorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ModelTests
{
    using System;
    using System.Linq;
    using Library.DomainLayer;
    using Library.DomainLayer.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AuthorTests.
    /// </summary>
    [TestClass]
    public class AuthorTests
    {
        /// <summary>
        /// The author.
        /// </summary>
        private Author author;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.author = new Author()
            {
                Id = 1,
                FirstName = "Mihail",
                LastName = "Sadoveanu",
            };
        }

        /// <summary>
        /// Defines the test method AuthorIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void AuthorIdShouldBeValid()
        {
            Assert.AreEqual(1, this.author.Id);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            bool isNameNull = this.author.LastName == null;
            Assert.IsFalse(isNameNull);

            bool isNameEmpty = this.author.LastName == string.Empty;
            Assert.IsFalse(isNameEmpty);

            var nameLength = this.author.LastName.Length;
            Assert.IsTrue(nameLength >= 2);
            Assert.IsTrue(nameLength <= 50);

            Assert.IsFalse(TestUtils.ContainsDigits(this.author.LastName));
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.author.LastName = null;
            bool isNameNull = this.author.LastName == null;
            Assert.IsTrue(isNameNull);

            this.author.LastName = string.Empty;
            bool isNameEmpty = this.author.LastName == string.Empty;
            Assert.IsTrue(isNameEmpty);

            this.author.LastName = "S";
            var nameLength = this.author.LastName.Length;
            Assert.IsFalse(nameLength >= 2);

            this.author.LastName = "Sadov12";
            int digits = this.author.LastName.Select(char.IsDigit).Count();
            Assert.IsTrue(digits != 0);
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            bool isNameNull = this.author.FirstName == null;
            Assert.IsFalse(isNameNull);

            bool isNameEmpty = this.author.FirstName == string.Empty;
            Assert.IsFalse(isNameEmpty);

            var nameLength = this.author.FirstName.Length;
            Assert.IsTrue(nameLength >= 2);
            Assert.IsTrue(nameLength <= 50);

            Assert.IsFalse(TestUtils.ContainsDigits(this.author.FirstName));
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.author.FirstName = null;
            bool isNameNull = this.author.FirstName == null;
            Assert.IsTrue(isNameNull);

            this.author.FirstName = string.Empty;
            bool isNameEmpty = this.author.FirstName == string.Empty;
            Assert.IsTrue(isNameEmpty);

            this.author.FirstName = "S";
            var nameLength = this.author.FirstName.Length;
            Assert.IsFalse(nameLength >= 2);

            this.author.FirstName = "Mihail12";
            bool containsDigits = this.author.FirstName.All(char.IsLetter);
            Assert.IsFalse(containsDigits);
        }
    }
}
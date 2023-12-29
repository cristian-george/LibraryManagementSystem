// <copyright file="AuthorTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests
{
    using System.Linq;
    using Library.DomainLayer;
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
            this.author = new ();
        }

        /// <summary>
        /// Defines the test method AuthorIdShouldBeValid.
        /// </summary>
        [TestMethod]
        public void AuthorIdShouldBeValid()
        {
            this.author.Id = 1;
            Assert.AreEqual(1, this.author.Id);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeValid()
        {
            this.author.LastName = "Fieraru";
            bool isIntString = this.author.LastName.All(char.IsLetter);
            Assert.IsTrue(isIntString);
        }

        /// <summary>
        /// Defines the test method LastNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void LastNameShouldBeInvalid()
        {
            this.author.LastName = "Fieraru123";
            bool isIntString = this.author.LastName.All(char.IsLetter);
            Assert.IsFalse(isIntString);
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeValid()
        {
            this.author.FirstName = "Cristian";
            bool isIntString = this.author.FirstName.All(char.IsLetter);
            Assert.IsTrue(isIntString);
        }

        /// <summary>
        /// Defines the test method FirstNameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void FirstNameShouldBeInvalid()
        {
            this.author.FirstName = "1223George";
            bool isIntString = this.author.FirstName.All(char.IsLetter);
            Assert.IsFalse(isIntString);
        }
    }
}
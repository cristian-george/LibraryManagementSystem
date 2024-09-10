// <copyright file="EditionTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Linq;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class EditionTests.
    /// </summary>
    [TestClass]
    public class EditionTests
    {
        /// <summary>
        /// The edition.
        /// </summary>
        private Edition edition;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.edition = new ();
        }

        /// <summary>
        /// Defines the test method PublisherShouldHaveLessThanFiftyChars.
        /// </summary>
        [TestMethod]
        public void PublisherShouldHaveLessThanFiftyChars()
        {
            var publisher = new string('a', 49);
            this.edition.Publisher = publisher;

            Assert.IsTrue(this.edition.Publisher.Length <= 50);
        }

        /// <summary>
        /// Defines the test method PublisherShouldHaveMoreThanFiftyChars.
        /// </summary>
        [TestMethod]
        public void PublisherShouldHaveMoreThanFiftyChars()
        {
            var publisher = new string('a', 52);
            this.edition.Publisher = publisher;

            Assert.IsTrue(this.edition.Publisher.Length > 50);
        }

        /// <summary>
        /// Defines the test method YearShouldBeValid.
        /// </summary>
        [TestMethod]
        public void YearShouldBeValid()
        {
            for (int year = 1850; year <= 2024; ++year)
            {
                this.edition.Year = year;
                Assert.IsTrue(this.edition.Year >= 1850);
                Assert.IsTrue(this.edition.Year <= 2024);
            }
        }

        /// <summary>
        /// Defines the test method YearShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void YearShouldBeInvalid()
        {
            this.edition.Year = 1849;
            Assert.IsFalse(this.edition.Year >= 1850);

            this.edition.Year = 2025;
            Assert.IsFalse(this.edition.Year <= 2024);
        }

        /// <summary>
        /// Defines the test method EditionNumberShouldBeLessThanFifty.
        /// </summary>
        [TestMethod]
        public void EditionNumberShouldBeLessThanFifty()
        {
            this.edition.EditionNumber = 23;

            Assert.IsTrue(this.edition.EditionNumber < 50);
        }

        /// <summary>
        /// Defines the test method EditionNumberShouldNotBeNegative.
        /// </summary>
        [TestMethod]
        public void EditionNumberShouldNotBeNegative()
        {
            this.edition.EditionNumber = -4;

            Assert.IsTrue(this.edition.EditionNumber < 0);
        }

        /// <summary>
        /// Defines the test method NumberOfPagesShouldNotBeNegative.
        /// </summary>
        [TestMethod]
        public void NumberOfPagesShouldNotBeNegative()
        {
            this.edition.NumberOfPages = -4;

            Assert.IsTrue(this.edition.NumberOfPages < 0);
        }
    }
}
// <copyright file="DomainTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class DomainTests.
    /// </summary>
    [TestClass]
    public class DomainTests
    {
        /// <summary>
        /// The domain.
        /// </summary>
        private Domain domain;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.domain = new Domain()
            {
                Id = 2,
                Name = "Algoritmica",
                ParentDomain = new Domain
                {
                    Id = 1,
                    Name = "Informatica",
                },
            };
        }

        /// <summary>
        /// Defines the test method NameShouldBeValid.
        /// </summary>
        [TestMethod]
        public void NameShouldBeValid()
        {
            Assert.IsNotNull(this.domain.Name);
            Assert.IsFalse(this.domain.Name.Equals(string.Empty));

            Assert.IsTrue(this.domain.Name.Length >= 2);
            Assert.IsTrue(this.domain.Name.Length <= 50);

            var flag = this.domain.Name.All(char.IsDigit);
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Defines the test method NameShouldBeInvalid.
        /// </summary>
        [TestMethod]
        public void NameShouldBeInvalid()
        {
            this.domain.Name = null;
            Assert.IsNull(this.domain.Name);

            this.domain.Name = string.Empty;
            Assert.IsTrue(this.domain.Name.Equals(string.Empty));

            this.domain.Name = "D";
            Assert.IsFalse(this.domain.Name.Length >= 2);
        }

        /// <summary>
        /// Defines the test method ParentShouldNotBeValid.
        /// </summary>
        [TestMethod]
        public void ParentShouldNotBeValid()
        {
            Assert.IsNotNull(this.domain.ParentDomain);
        }

        /// <summary>
        /// Defines the test method ParentShouldNotBeInvalid.
        /// </summary>
        [TestMethod]
        public void ParentShouldNotBeInvalid()
        {
            this.domain.ParentDomain = null;
            Assert.IsNull(this.domain.ParentDomain);
        }
    }
}
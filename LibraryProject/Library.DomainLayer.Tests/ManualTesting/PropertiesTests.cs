// <copyright file="PropertiesTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests.ManualTesting
{
    using Library.DomainLayer.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class PropertiesTests.
    /// </summary>
    [TestClass]
    public class PropertiesTests
    {
        /// <summary>
        /// The properties.
        /// </summary>
        private Properties properties;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.properties = new Properties()
            {
                Domenii = 2,
                Nmc = 5,
                L = 2,
                C = 3,
                D = 5,
                Lim = 6,
                Delta = 7,
                Ncz = 3,
                Persimp = 5,
                Per = 4,
            };
        }

        /// <summary>
        /// Defines the test method DomeniiShouldBeValid.
        /// </summary>
        [TestMethod]
        public void DomeniiShouldBeValid()
        {
            Assert.IsTrue(this.properties.Domenii > 0);
        }

        /// <summary>
        /// Defines the test method NmcShouldBeValid.
        /// </summary>
        [TestMethod]
        public void NmcShouldBeValid()
        {
            Assert.IsTrue(this.properties.Nmc > 0);
        }

        /// <summary>
        /// Defines the test method LShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LShouldBeValid()
        {
            Assert.IsTrue(this.properties.L > 0);
        }

        /// <summary>
        /// Defines the test method CShouldBeValid.
        /// </summary>
        [TestMethod]
        public void CShouldBeValid()
        {
            Assert.IsTrue(this.properties.C > 0);
        }

        /// <summary>
        /// Defines the test method DShouldBeValid.
        /// </summary>
        [TestMethod]
        public void DShouldBeValid()
        {
            Assert.IsTrue(this.properties.D > 0);
        }

        /// <summary>
        /// Defines the test method LimShouldBeValid.
        /// </summary>
        [TestMethod]
        public void LimShouldBeValid()
        {
            Assert.IsTrue(this.properties.Lim > 0);
        }

        /// <summary>
        /// Defines the test method DeltaShouldBeValid.
        /// </summary>
        [TestMethod]
        public void DeltaShouldBeValid()
        {
            Assert.IsTrue(this.properties.Delta > 0);
        }

        /// <summary>
        /// Defines the test method NczShouldBeValid.
        /// </summary>
        [TestMethod]
        public void NczShouldBeValid()
        {
            Assert.IsTrue(this.properties.Ncz > 0);
        }

        /// <summary>
        /// Defines the test method PersimpShouldBeValid.
        /// </summary>
        [TestMethod]
        public void PersimpShouldBeValid()
        {
            Assert.IsTrue(this.properties.Persimp > 0);
        }

        /// <summary>
        /// Defines the test method PerShouldBeValid.
        /// </summary>
        [TestMethod]
        public void PerShouldBeValid()
        {
            Assert.IsTrue(this.properties.Per > 0);
        }
    }
}

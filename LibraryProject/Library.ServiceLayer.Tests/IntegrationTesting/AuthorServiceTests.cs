// <copyright file="AuthorServiceTests.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests.IntegrationTesting
{
    using System.Linq;
    using Library.Injection;
    using Library.ServiceLayer.Services;
    using Library.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines test class AuthorServiceTests.
    /// </summary>
    [TestClass]
    public class AuthorServiceTests
    {
        private AuthorService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Injector.Initialize();
            this.service = Injector.Create<AuthorService>();
        }

        /// <summary>
        /// Defines the test method EndToEndAuthor.
        /// </summary>
        [TestMethod]
        public void EndToEndAuthor()
        {
            var book = ProduceModel.GetBookModel();
            var author = book.Authors.First();

            // Insert
            Assert.IsTrue(this.service.Insert(author));

            // GetAll
            var allAuthors = this.service.Get();
            Assert.IsNotNull(allAuthors);

            // GetById
            var id = 1;
            var dbAuthor = this.service.GetById(id);
            Assert.IsNull(dbAuthor);
            Assert.IsFalse(this.service.DeleteById(id));

            id = allAuthors.LastOrDefault().Id;
            dbAuthor = this.service.GetById(id);
            Assert.IsNotNull(dbAuthor);

            // Update
            dbAuthor.LastName = "Garcea";
            Assert.IsTrue(this.service.Update(dbAuthor));

            // Delete
            Assert.IsTrue(this.service.DeleteById(dbAuthor.Id));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Clean table
            Assert.IsTrue(this.service.Delete());
        }
    }
}

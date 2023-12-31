// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators.BookValidators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class BookService.
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Book, IBookRepository}" />
    /// Implements the <see cref="IBookService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Book, IBookRepository}" />
    /// <seealso cref="IBookService" />
    public class BookService : BaseService<Book, IBookRepository, IPropertiesRepository>, IBookService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService" /> class.
        /// </summary>
        public BookService()
            : base(Injector.Create<IBookRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new BookValidator();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>
        public override bool Insert(Book entity)
        {
            if (entity.Authors.Count == 0)
            {
                this.Validator = new BookWithoutAuthorsValidator();
            }

            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckFlags(entity))
            {
                _ = this.Repository.Insert(entity);
            }
            else
            {
                _ = LogUtils.LogErrors(result);
                return false;
            }

            this.Validator = new BookValidator();
            return true;
        }

        /// <summary>
        /// O carte nu poate face parte din mai mult de DOMENII domenii.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> bool. </returns>
        private bool CheckIfInMoreThanDOMENIIDomains(Book book)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            return book.Domains.Count > properties.DOMENII;
        }

        /// <summary>
        /// Checks the flags.
        /// </summary>
        /// <param name="book"> The book. </param>
        private bool CheckFlags(Book book)
        {
            if (this.CheckIfInMoreThanDOMENIIDomains(book))
            {
                return false;
            }

            if (!BookServiceUtils.BookHasCorrectDomains(book))
            {
                return false;
            }

            BookServiceUtils.AddAncestorDomains(book);
            return true;
        }
    }
}
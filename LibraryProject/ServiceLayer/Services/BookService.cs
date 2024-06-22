// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators.BookValidators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class BookService.
    /// Implements the <see cref="Services.BaseService{Book, IBookRepository}" />
    /// Implements the <see cref="IBookService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{Book, IBookRepository}" />
    /// <seealso cref="IBookService" />
    public class BookService : BaseService<Book, IBookRepository>, IBookService
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
            else
            {
                this.Validator = new BookValidator();
            }

            var result = this.Validator.Validate(entity);
            if (result.IsValid && this.CheckBookAdditionalRules(entity))
            {
                _ = this.Repository.Insert(entity);
            }
            else
            {
                LogUtils.LogErrors(result);
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool IsInAtLeastOneDomain(Book book)
        {
            return book.Domains.Count > 0;
        }

        /// <inheritdoc/>
        public bool IsInTheCorrectDomains(Book book)
        {
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                var rootDomain = DomainServiceUtils.GetRootDomain(domain);
                domains.Add(rootDomain);
            }

            return domains.DistinctBy(domain => domain.Name).Count() == domains.Count;
        }

        /// <inheritdoc/>
        public bool IsInMoreThanDOMENIIDomains(Book book)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            return book.Domains.Count > properties.DOMENII;
        }

        /// <summary>
        /// Checks book additional rules.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if all book additional rules succeed, <c>false</c> otherwise.</returns>
        private bool CheckBookAdditionalRules(Book book)
        {
            // Face parte din cel putin un domeniu
            if (!this.IsInAtLeastOneDomain(book))
            {
                return false;
            }

            // O carte nu poate face parte din mai mult de DOMENII domenii.
            if (this.IsInMoreThanDOMENIIDomains(book))
            {
                return false;
            }

            // Se va verifica faptul ca o carte nu poate sa se specifice explicit
            // ca fiind din domenii aflate in relatia stramos-descendent.
            if (!this.IsInTheCorrectDomains(book))
            {
                return false;
            }

            // Daca o carte face parte dintr-un subdomeniu, automat va fi regasita
            // si ca facand parte din domeniile stramos, fara ca acest lucru
            // sa fie declarat explicit in incadrarea initiala a cartii.
            BookServiceUtils.AddAncestorDomains(book);
            return true;
        }
    }
}
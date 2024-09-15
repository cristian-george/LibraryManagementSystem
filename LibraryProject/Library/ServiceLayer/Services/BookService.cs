// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
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

        /// <inheritdoc/>
        public override bool Insert(Book entity)
        {
            var result = this.Validator.Validate(entity);
            if (!result.IsValid)
            {
                Logging.LogErrors(result);
                return false;
            }

            if (!this.CheckAdditionalRules(entity))
            {
                return false;
            }

            // If a book belongs to a subdomain, it will automatically be considered
            // as part of the parent domains as well.
            entity.AddAncestorDomains();

            return this.Repository.Insert(entity);
        }

        /// <inheritdoc/>
        public bool IsInMoreThanNDomains(Book book)
        {
            var properties = this.PropertiesRepository.GetLast();

            return book.Domains.Count > properties.Domenii;
        }

        /// <inheritdoc/>
        public bool IsInParentChildRelationDomains(Book book)
        {
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                var rootDomain = domain.GetRootDomain();
                domains.Add(rootDomain);
            }

            var rootDomainsCount = domains
                .DistinctBy(domain => domain.Name)
                .Count();

            return rootDomainsCount != domains.Count;
        }

        /// <summary>
        /// Checks book additional rules.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if all book additional rules succeed, <c>false</c> otherwise.</returns>
        private bool CheckAdditionalRules(Book book)
        {
            if (this.Repository.GetByTitle(book.Title) != null)
            {
                return false;
            }

            if (this.IsInMoreThanNDomains(book))
            {
                return false;
            }

            if (this.IsInParentChildRelationDomains(book))
            {
                return false;
            }

            return true;
        }
    }
}
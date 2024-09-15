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
            if (result.IsValid && this.CheckAdditionalRules(entity))
            {
                // Daca o carte face parte dintr-un subdomeniu, automat va fi regasita
                // si ca facand parte din domeniile stramos, fara ca acest lucru
                // sa fie declarat explicit in incadrarea initiala a cartii.
                entity.AddAncestorDomains();

                _ = this.Repository.Insert(entity);
            }
            else
            {
                Logging.LogErrors(result);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a book is in more than N domains.
        /// N is a threshold for number of domains.
        /// </summary>
        /// <param name="book">A Book.</param>
        /// <returns>Bool.</returns>
        public bool IsInMoreThanNDomains(Book book)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            return book.Domains.Count > properties.Domenii;
        }

        /// <summary>
        /// Checks if a book is in parent-child relation domains.
        /// </summary>
        /// <param name="book">A book.</param>
        /// <returns>Bool.</returns>
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
        /// <returns>Bool.</returns>
        public bool CheckAdditionalRules(Book book)
        {
            if (this.Repository.GetBookByTitle(book.Title) != null)
            {
                return false;
            }

            // O carte nu poate face parte din mai mult de n domenii.
            if (this.IsInMoreThanNDomains(book))
            {
                return false;
            }

            // Se va verifica faptul ca o carte nu poate sa se specifice explicit
            // ca fiind din domenii aflate in relatia stramos-descendent.
            if (this.IsInParentChildRelationDomains(book))
            {
                return false;
            }

            return true;
        }
    }
}
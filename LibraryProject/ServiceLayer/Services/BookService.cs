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
            this.Validator = new BookValidator();

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
        /// Checks book additional rules.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>bool.</returns>
        public bool CheckAdditionalRules(Book book)
        {
            if (this.Repository.GetBookByTitle(book.Title) != null)
            {
                return false;
            }

            var properties = this.PropertiesRepository.GetLastProperties();

            // O carte nu poate face parte din mai mult de n domenii.
            if (book.Domains.Count > properties.Domenii)
            {
                return false;
            }

            // Se va verifica faptul ca o carte nu poate sa se specifice explicit
            // ca fiind din domenii aflate in relatia stramos-descendent.
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                var rootDomain = domain.GetRootDomain();
                domains.Add(rootDomain);
            }

            var rootDomainsCount = domains
                .DistinctBy(domain => domain.Name)
                .Count();

            if (rootDomainsCount != domains.Count)
            {
                return false;
            }

            return true;
        }
    }
}
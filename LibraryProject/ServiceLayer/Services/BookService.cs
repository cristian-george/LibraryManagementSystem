// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System.Collections.Generic;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators.BookValidators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.IServices;

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
                _ = Utils.LogErrors(result);
                return false;
            }

            this.Validator = new BookValidator();
            return true;
        }

        /// <summary>
        ///  Se va verifica faptul ca o carte nu poate sa se specifice explicit
        ///  ca fiind din domenii aflate in relatia stramos-descendent.
        /// The books has correct domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> bool. </returns>
        public bool BookHasCorrectDomains(Book book)
        {
            var domainsList = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                this.GetDomainsWithoutTheFirst(domain, domainsList);
                foreach (var parentDomain in domainsList)
                {
                    if (domain.Id == parentDomain.Id)
                    {
                        return false;
                    }
                }

                domainsList.Clear();
            }

            return true;
        }

        /// <summary>
        /// Daca o carte face parte dintr-un subdomeniu, automat va fi regasita si ca facand parte
        /// din domeniile stramos, fara ca acest lucru sa fie declarat explicit in incadrarea initiala a cartii
        /// Adds the ancestor domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        public void AddAncestorDomains(Book book)
        {
            book.Domains = this.GetDomainsList(book);
        }

        /// <summary>
        /// Gets the domains list.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> List of Domain. </returns>
        public List<Domain> GetDomainsList(Book book)
        {
            var domainsList = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                this.GetDomainsWithTheFirst(domain, domainsList);
            }

            return domainsList;
        }

        /// <summary>
        /// Gets the domains without the first.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        private void GetDomainsWithoutTheFirst(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                return;
            }

            domains.Add(domain.ParentDomain);
            this.GetDomainsWithoutTheFirst(domain.ParentDomain, domains);
        }

        /// <summary>
        /// Gets the domains with the first.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        private void GetDomainsWithTheFirst(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                domains.Add(domain);
                return;
            }

            domains.Add(domain.ParentDomain);
            this.GetDomainsWithTheFirst(domain.ParentDomain, domains);
        }

        /// <summary>
        /// Checks the flags.
        /// </summary>
        /// <param name="book"> The book. </param>
        private bool CheckFlags(Book book)
        {
            var properties = this.PropertiesRepository.GetLastProperties();

            if (book.Domains.Count > properties.DOMENII)
            {
                return false;
            }

            if (this.BookHasCorrectDomains(book) == false)
            {
                return false;
            }

            this.AddAncestorDomains(book);
            return true;
        }
    }
}
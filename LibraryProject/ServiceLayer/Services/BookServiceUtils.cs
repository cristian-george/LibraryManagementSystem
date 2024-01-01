// <copyright file="BookServiceUtils.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System.Collections.Generic;
    using Library.DomainLayer;

    /// <summary>
    /// Class BookServiceUtils.
    /// </summary>
    public static class BookServiceUtils
    {
        /// <summary>
        /// Adds ancestor domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        public static void AddAncestorDomains(Book book)
        {
            book.Domains = GetDomains(book);
        }

        /// <summary>
        /// Gets the domains without the parent domain.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        public static void GetDomainsWithoutTheParent(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                return;
            }

            domains.Add(domain.ParentDomain);
            GetDomainsWithoutTheParent(domain.ParentDomain, domains);
        }

        /// <summary>
        /// Gets the list of domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> List of Domain. </returns>
        private static List<Domain> GetDomains(Book book)
        {
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                GetDomainsWithTheParent(domain, domains);
            }

            return domains;
        }

        /// <summary>
        /// Gets the domains with the parent domain.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        private static void GetDomainsWithTheParent(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                domains.Add(domain);
                return;
            }

            domains.Add(domain.ParentDomain);
            GetDomainsWithTheParent(domain.ParentDomain, domains);
        }
    }
}

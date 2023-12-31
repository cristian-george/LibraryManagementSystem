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
        ///  Se va verifica faptul ca o carte nu poate sa se specifice explicit
        ///  ca fiind din domenii aflate in relatia stramos-descendent.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> bool. </returns>
        public static bool BookHasCorrectDomains(Book book)
        {
            var domainsList = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                GetDomainsWithoutTheFirst(domain, domainsList);
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
        /// din domeniile stramos, fara ca acest lucru sa fie declarat explicit in incadrarea initiala a cartii.
        /// </summary>
        /// <param name="book"> The book. </param>
        public static void AddAncestorDomains(Book book)
        {
            book.Domains = GetDomainsList(book);
        }

        /// <summary>
        /// Gets the domains list.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> List of Domain. </returns>
        private static List<Domain> GetDomainsList(Book book)
        {
            var domainsList = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                GetDomainsWithTheFirst(domain, domainsList);
            }

            return domainsList;
        }

        /// <summary>
        /// Gets the domains without the first.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        private static void GetDomainsWithoutTheFirst(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                return;
            }

            domains.Add(domain.ParentDomain);
            GetDomainsWithoutTheFirst(domain.ParentDomain, domains);
        }

        /// <summary>
        /// Gets the domains with the first.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        private static void GetDomainsWithTheFirst(Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                domains.Add(domain);
                return;
            }

            domains.Add(domain.ParentDomain);
            GetDomainsWithTheFirst(domain.ParentDomain, domains);
        }
    }
}

// <copyright file="BookExtensions.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Extensions for Book model.
    /// </summary>
    public static class BookExtensions
    {
        /// <summary>
        /// Adds ancestor domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        public static void AddAncestorDomains(this Book book)
        {
            var domains = book.GetAncestorDomains();

            foreach (var domain in domains)
            {
                book.Domains.Add(domain);
            }
        }

        /// <summary>
        /// Gets the list of domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> List of Domain. </returns>
        private static List<Domain> GetAncestorDomains(this Book book)
        {
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                domain.GetDomainsWithTheRootDomain(domains);
            }

            return domains
                .DistinctBy(d => d.Name)
                .ToList();
        }
    }
}
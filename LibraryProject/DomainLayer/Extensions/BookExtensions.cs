// <copyright file="BookExtensions.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Extensions
{
    using System.Collections.Generic;
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
            book.Domains = GetDomains(book);
        }

        /// <summary>
        /// Gets the list of domains.
        /// </summary>
        /// <param name="book"> The book. </param>
        /// <returns> List of Domain. </returns>
        private static List<Domain> GetDomains(this Book book)
        {
            var domains = new List<Domain>();

            foreach (var domain in book.Domains)
            {
                domain.GetDomainsWithTheParent(domains);
            }

            return domains;
        }
    }
}
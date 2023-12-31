// <copyright file="DomainServiceUtils.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DomainLayer;

    /// <summary>
    /// Class DomainServiceUtils.
    /// </summary>
    public static class DomainServiceUtils
    {
        /// <summary>
        /// Gets the no of distinct categories.
        /// </summary>
        /// <param name="domains">The domains.</param>
        /// <returns>System.Int32.</returns>
        public static int GetNoOfDistinctDomains(ICollection<Domain> domains)
        {
            var listOfParentDomains = new List<Domain>();

            foreach (var domain in domains)
            {
                listOfParentDomains.Add(GetParentDomain(domain));
            }

            listOfParentDomains = listOfParentDomains.Distinct().ToList();

            return listOfParentDomains.Count;
        }

        /// <summary>
        /// Gets the parent domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>Domain.</returns>
        public static Domain GetParentDomain(Domain domain)
        {
            while (domain.ParentDomain != null)
            {
                domain = domain.ParentDomain;
            }

            return domain;
        }
    }
}

// <copyright file="DomainExtensions.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Extensions
{
    using System.Collections.Generic;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Extensions for Domain model.
    /// </summary>
    public static class DomainExtensions
    {
        /// <summary>
        /// Gets the parent domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>Domain.</returns>
        public static Domain GetRootDomain(this Domain domain)
        {
            if (domain.ParentDomain == null)
            {
                return domain;
            }

            Domain rootDomain = domain;

            do
            {
                rootDomain = rootDomain.ParentDomain;
            }
            while (rootDomain.ParentDomain != null);

            return rootDomain;
        }

        /// <summary>
        /// Sets all children to the parent recursively.
        /// </summary>
        /// <param name="parent">Domain.</param>
        public static void SetParentDomain(this Domain parent)
        {
            foreach (var child in parent.ChildrenDomains)
            {
                child.ParentDomain = parent;
                SetParentDomain(child);
            }
        }

        /// <summary>
        /// Gets the domains with the root domain.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="domains"> The domains. </param>
        public static void GetDomainsWithTheRootDomain(this Domain domain, List<Domain> domains)
        {
            if (domain.ParentDomain == null)
            {
                domains.Add(domain);
                return;
            }

            domains.Add(domain.ParentDomain);
            domain.ParentDomain.GetDomainsWithTheRootDomain(domains);
        }
    }
}
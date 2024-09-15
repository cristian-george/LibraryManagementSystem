// <copyright file="IDomainRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Interfaces
{
    using Library.DataLayer;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Domain repository interface.
    /// </summary>
    public interface IDomainRepository : IRepository<Domain>
    {
        /// <summary>
        /// Gets domain by name.
        /// </summary>
        /// <param name="name">Domain name.</param>
        /// <returns>Domain.</returns>
        Domain GetDomainByName(string name);
    }
}
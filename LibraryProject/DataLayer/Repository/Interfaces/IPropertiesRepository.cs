// <copyright file="IPropertiesRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IPropertiesRepository
    /// Implements the <see cref="IRepository{Properties}" />.
    /// </summary>
    /// <seealso cref="IRepository{Properties}" />
    public interface IPropertiesRepository : IRepository<Properties>
    {
        /// <summary>
        /// Gets the last properties.
        /// </summary>
        /// <returns> Properties. </returns>
        public Properties GetLastProperties();
    }
}
// <copyright file="IPropertiesRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Properties repository interface.
    /// </summary>
    public interface IPropertiesRepository : IRepository<Properties>
    {
        /// <summary>
        /// Gets the last properties.
        /// </summary>
        /// <returns> Properties. </returns>
        public Properties GetLastProperties();
    }
}
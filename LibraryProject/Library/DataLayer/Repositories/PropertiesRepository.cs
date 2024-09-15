// <copyright file="PropertiesRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Properties repository.
    /// </summary>
    public class PropertiesRepository : BaseRepository<Properties>, IPropertiesRepository
    {
        /// <summary>
        /// Gets the last properties.
        /// </summary>
        /// <returns>Properties.</returns>
        public Properties GetLast()
        {
            var properties = this.Get()
                .OrderByDescending(properties => properties.Id)
                .FirstOrDefault();

            return properties;
        }
    }
}
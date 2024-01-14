// <copyright file="PropertiesRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Concretes namespace.
/// </summary>
namespace Library.DataLayer.Repository.Concretes
{
    using System;
    using System.Linq;
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// Class PropertiesRepository.
    /// Implements the <see cref="BaseRepository{Properties}" />
    /// Implements the <see cref="IPropertiesRepository" />.
    /// </summary>
    /// <seealso cref="BaseRepository{Properties}" />
    /// <seealso cref="IPropertiesRepository" />
    public class PropertiesRepository : BaseRepository<Properties>, IPropertiesRepository
    {
        /// <summary>
        /// Gets the last properties.
        /// </summary>
        /// <returns> Properties. </returns>
        public Properties GetLastProperties()
        {
            var lastPropertiesId = this.Ctx.Properties.Max(x => x.Id);
            return this.Ctx.Properties.FirstOrDefault(x => x.Id == lastPropertiesId);
        }
    }
}
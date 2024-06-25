// <copyright file="PropertiesRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using System.Linq;
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
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
        public Properties GetLastProperties()
        {
            var lastPropertiesId = this.Ctx.Properties.Max(x => x.Id);
            return this.Ctx.Properties.FirstOrDefault(x => x.Id == lastPropertiesId);
        }
    }
}
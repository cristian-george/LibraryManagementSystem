// <copyright file="DomainRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Domain repository.
    /// </summary>
    public class DomainRepository : BaseRepository<Domain>, IDomainRepository
    {
        /// <inheritdoc/>
        public Domain GetByName(string name)
        {
            var domain = this.Get(
                filterBy: domain => domain.Name.Equals(name))
                .SingleOrDefault();

            return domain;
        }
    }
}
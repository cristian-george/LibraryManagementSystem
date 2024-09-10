// <copyright file="EditionRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Ninject.Infrastructure.Language;

    /// <summary>
    /// Edition repository.
    /// </summary>
    public class EditionRepository : BaseRepository<Edition>, IEditionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Edition> GetBookEditionsById(int bookId)
        {
            var editions = this.Ctx.Editions
                .Where(e => e.Book.Id == bookId)
                .ToEnumerable();

            return editions;
        }
    }
}
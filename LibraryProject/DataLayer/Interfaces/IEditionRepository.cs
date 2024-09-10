// <copyright file="IEditionRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Interfaces
{
    using System.Collections.Generic;
    using Library.DataLayer;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition repository interface.
    /// </summary>
    public interface IEditionRepository : IRepository<Edition>
    {
        /// <summary>
        /// Gets book's editions by id.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <returns>Editions.</returns>
        IEnumerable<Edition> GetBookEditionsById(int bookId);
    }
}
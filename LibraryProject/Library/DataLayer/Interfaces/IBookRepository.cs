// <copyright file="IBookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Interfaces
{
    using Library.DataLayer;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book repository interface.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets book by title.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <returns>Book.</returns>
        Book GetByTitle(string title);

        /// <summary>
        /// Gets book by stock stockId.
        /// </summary>
        /// <param name="stockId">Stock stockId.</param>
        /// <returns>Book.</returns>
        Book GetByStockId(int stockId);
    }
}
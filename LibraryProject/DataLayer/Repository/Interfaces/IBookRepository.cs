// <copyright file="IBookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book repository interface.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets book by stock id.
        /// </summary>
        /// <param name="stockId">Stock id.</param>
        /// <returns>Book.</returns>
        Book GetBookByStockId(int stockId);
    }
}
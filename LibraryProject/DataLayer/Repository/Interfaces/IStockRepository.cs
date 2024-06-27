// <copyright file="IStockRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using System.Collections.Generic;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock repository interface.
    /// </summary>
    public interface IStockRepository : IRepository<Stock>
    {
        /// <summary>
        /// Gets stocks by book id.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <returns>Stocks.</returns>
        IEnumerable<Stock> GetStocksByBookId(int bookId);
    }
}
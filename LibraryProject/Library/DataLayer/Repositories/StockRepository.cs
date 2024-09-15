// <copyright file="StockRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Collections.Generic;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock repository.
    /// </summary>
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Stock> GetByBookId(int id)
        {
            var stocks = this.Get(
                filterBy: stock => stock.Edition.Book.Id == id);

            return stocks;
        }
    }
}
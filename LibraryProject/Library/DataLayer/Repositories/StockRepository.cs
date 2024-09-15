// <copyright file="StockRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Microsoft.EntityFrameworkCore;
    using Ninject.Infrastructure.Language;

    /// <summary>
    /// Stock repository.
    /// </summary>
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Stock> GetStocksByBookId(int bookId)
        {
            var stocks = this.Ctx.Stocks
                .Include(s => s.Edition)
                .ThenInclude(e => e.Book)
                .Where(s => s.Edition.Book.Id == bookId)
                .ToEnumerable();

            return stocks;
        }
    }
}
// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using System.Linq;
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Book repository.
    /// </summary>
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <inheritdoc/>
        public Book GetBookByStockId(int stockId)
        {
            var book = this.Ctx.Stocks
            .Where(s => s.Id == stockId)
            .Select(s => s.Edition.Book)
            .FirstOrDefault();

            return book;
        }
    }
}
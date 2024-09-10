// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Book repository.
    /// </summary>
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <inheritdoc/>
        public Book GetBookByTitle(string title)
        {
            var book = this.Ctx.Books
                .Where(book => book.Title == title)
                .Include(book => book.Authors)
                .Include(book => book.Domains)
                .Include(book => book.Editions)
                .FirstOrDefault();

            return book;
        }

        /// <inheritdoc/>
        public Book GetBookByStockId(int stockId)
        {
            var book = this.Ctx.Stocks
                .Where(stock => stock.Id == stockId)
                .Include(stock => stock.Edition)
                    .ThenInclude(edition => edition.Book)
                        .ThenInclude(book => book.Authors)
                .Include(stock => stock.Edition)
                    .ThenInclude(edition => edition.Book)
                        .ThenInclude(book => book.Domains)
                .Include(stock => stock.Edition)
                    .ThenInclude(edition => edition.Book)
                        .ThenInclude(book => book.Editions)
                .Select(stock => stock.Edition.Book)
                .FirstOrDefault();

            return book;
        }
    }
}
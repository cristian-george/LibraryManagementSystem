// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book repository.
    /// </summary>
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <inheritdoc/>
        public Book GetByTitle(string title)
        {
            var book = this.Get(
                filterBy: book => book.Title == title)
                .FirstOrDefault();

            return book;
        }

        /// <inheritdoc/>
        public Book GetByStockId(int stockId)
        {
            var book = this.Get(
                filterBy: book => book.Editions.Any(edition => edition.Stocks.Any(s => s.Id == stockId)))
                .FirstOrDefault();

            return book;
        }
    }
}
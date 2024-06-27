// <copyright file="BorrowExtensions.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Extensions
{
    using System.Collections.Generic;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Extensions for Borrow model.
    /// </summary>
    public static class BorrowExtensions
    {
        /// <summary>
        /// Get the books to borrow.
        /// </summary>
        /// <param name="borrow">Borrow.</param>
        /// <param name="bookRepository">BookRepository.</param>
        /// <returns>Books.</returns>
        public static IEnumerable<Book> GetBorrowedBooks(this Borrow borrow, IBookRepository bookRepository)
        {
            var booksToBorrow = new List<Book>();

            foreach (var stock in borrow.Stocks)
            {
                var book = bookRepository.GetBookByStockId(stock.Id);
                booksToBorrow.Add(book);
            }

            return booksToBorrow;
        }
    }
}

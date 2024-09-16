// <copyright file="IBorrowRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Library.DataLayer;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Borrow repository interface.
    /// </summary>
    public interface IBorrowRepository : IRepository<Borrow>
    {
        /// <summary>
        /// Gets borrows by reader id.
        /// </summary>
        /// <param name="readerId">Reader id.</param>
        /// <returns>Borrows.</returns>
        IEnumerable<Borrow> GetBorrowsByReader(int readerId);

        /// <summary>
        /// Gets borrows made by a reader within date.
        /// </summary>
        /// <param name="readerId">Reader id.</param>
        /// <param name="date">Date.</param>
        /// <returns>Borrows.</returns>
        IEnumerable<Borrow> GetBorrowsByReaderWithinDate(int readerId, DateTime date);

        /// <summary>
        /// Gets specific book borrows by a reader.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="readerId">Reader id.</param>
        /// <returns>int.</returns>
        IEnumerable<Borrow> GetBorrowsOfBookByReader(int bookId, int readerId);

        /// <summary>
        /// Gets how many times a book has been borrowed by a reader within date.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="readerId">Reader id.</param>
        /// <param name="date">Date.</param>
        /// <returns>int.</returns>
        int GetBorrowCountOfBookByReaderWithinDate(int bookId, int readerId, DateTime date);

        /// <summary>
        /// Gets last borrow of a book by a reader.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="readerId">Reader id.</param>
        /// <returns>Borrow.</returns>
        Borrow GetLastBorrowOfBookByReader(int bookId, int readerId);
    }
}
﻿// <copyright file="IBorrowRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
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
        IEnumerable<Borrow> GetBorrowsByReaderId(int readerId);

        /// <summary>
        /// Gets how many times a book has been borrowed by a reader from date.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="readerId">Reader id.</param>
        /// <param name="date">Date.</param>
        /// <returns>int.</returns>
        int GetBookBorrowCountByReader(int bookId, int readerId, DateTime? date = null);

        /// <summary>
        /// Gets last borrow of a book by a reader.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="readerId">Reader id.</param>
        /// <returns>Borrow.</returns>
        Borrow GetLastBookBorrowByReader(int bookId, int readerId);
    }
}
﻿// <copyright file="BorrowRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Borrow repository.
    /// </summary>
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsByReader(int readerId)
        {
            var borrows = this.Get(
                filterBy: borrow => borrow.Reader.Id == readerId);

            return borrows;
        }

        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsByReaderWithinDate(int readerId, DateTime date)
        {
            var borrows = this.GetBorrowsByReader(readerId)
                .Where(borrow => borrow.BorrowDate >= date);

            return borrows;
        }

        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsOfBookByReader(int bookId, int readerId)
        {
            var borrows = this.Get(
                filterBy: borrow => borrow.Reader.Id == readerId &&
                                    borrow.Stocks.Any(stock => stock.Edition.Book.Id == bookId));

            return borrows;
        }

        /// <inheritdoc/>
        public int GetBorrowCountOfBookByReaderWithinDate(int bookId, int readerId, DateTime date)
        {
            var counter = this.GetBorrowsOfBookByReader(bookId, readerId)
                .Where(borrow => borrow.BorrowDate >= date)
                .SelectMany(borrow => borrow.Stocks)
                .Count();

            return counter;
        }

        /// <inheritdoc/>
        public Borrow GetLastBorrowOfBookByReader(int bookId, int readerId)
        {
            var borrow = this.Get(
                filterBy: borrow => borrow.Reader.Id == readerId &&
                                    borrow.Stocks.Any(stock => stock.Edition.Book.Id == bookId),
                orderBy: query => query.OrderByDescending(borrow => borrow.BorrowDate))
                .FirstOrDefault();

            return borrow;
        }
    }
}
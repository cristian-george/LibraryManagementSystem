// <copyright file="BorrowRepository.cs" company="Transilvania University of Brasov">
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
    using Microsoft.EntityFrameworkCore;
    using Ninject.Infrastructure.Language;

    /// <summary>
    /// Borrow repository.
    /// </summary>
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsByReader(int readerId)
        {
            var borrows = this.Ctx.Borrows
                .Where(borrow => borrow.ReaderId == readerId)
                .Include(borrow => borrow.Stocks)
                .ToEnumerable();

            return borrows;
        }

        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsByReaderWithinDate(int readerId, DateTime date)
        {
            var borrows = this.GetBorrowsByReader(readerId)
                .Where(b => b.BorrowDate >= date);

            return borrows;
        }

        /// <inheritdoc/>
        public int GetBookBorrowCountByReader(int bookId, int readerId)
        {
            var borrowCount = this.Ctx.Borrows
                .Where(borrow => borrow.ReaderId == readerId)
                .Include(b => b.Stocks
                    .Select(stock => stock.Edition)
                        .Select(edition => edition.Book))
                .SelectMany(b => b.Stocks)
                .Count(s => s.Edition.Book.Id == bookId);

            return borrowCount;
        }

        /// <inheritdoc/>
        public int GetBookBorrowCountByReaderWithinDate(int bookId, int readerId, DateTime date)
        {
            var borrowCount = this.Ctx.Borrows
                .Where(borrow => borrow.ReaderId == readerId && borrow.BorrowDate >= date)
                .Include(b => b.Stocks
                    .Select(stock => stock.Edition)
                        .Select(edition => edition.Book))
                .SelectMany(b => b.Stocks)
                .Count(s => s.Edition.Book.Id == bookId);

            return borrowCount;
        }

        /// <inheritdoc/>
        public Borrow GetLastBookBorrowByReader(int bookId, int readerId)
        {
            var lastBorrow = this.Ctx.Borrows
            .Where(b => b.ReaderId == readerId)
            .SelectMany(b => b.Stocks
                .Where(s => s.Edition.Book.Id == bookId)
                .Select(s => b))
            .OrderByDescending(b => b.ReturnDate)
            .FirstOrDefault();

            return lastBorrow;
        }
    }
}
// <copyright file="BorrowRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer.Models;
    using Microsoft.EntityFrameworkCore;
    using Ninject.Infrastructure.Language;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    /// <summary>
    /// Borrow repository.
    /// </summary>
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Borrow> GetBorrowsByReaderId(int readerId)
        {
            var borrows = this.Ctx.Borrows
                .Where(b => b.ReaderId == readerId)
                .ToEnumerable();

            return borrows;
        }

        /// <inheritdoc/>
        public int GetBookBorrowCountByReader(int bookId, int readerId, DateTime? date = null)
        {
            var borrowCount = this.Ctx.Borrows
                .Where(b => b.ReaderId == readerId && (date == null || b.BorrowDate >= date))
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
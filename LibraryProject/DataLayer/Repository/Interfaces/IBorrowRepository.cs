// <copyright file="IBorrowRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the borrow controller.
    /// </summary>
    public interface IBorrowRepository : IRepository<Borrow>
    {
        /// <summary>
        /// Gets the first borrow date.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> DateTime. </returns>
        public DateTime GetFirstBorrowDate(int id);

        /// <summary>
        /// Gets the number of borrows today.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// /// <returns> int. </returns>
        public int GetNumberOfBorrowsToday(int id);

        /// <summary>
        /// Gets the books between past months and present.
        /// </summary>
        /// <param name="months"> The months. </param>
        /// /// <returns> IEnumerable of Borrow. </returns>
        public IEnumerable<Borrow> GetBooksBetweenPastMonthsAndPresent(int months);
    }
}
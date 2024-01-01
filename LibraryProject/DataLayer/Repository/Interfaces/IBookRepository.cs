﻿// <copyright file="IBookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using System.Collections.Generic;
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the book controller.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets the unavailable books.
        /// </summary>
        /// <param name="allBooksWithTheSameName"> Books with the same name. </param>
        /// <returns> IEnumerable of Book. </returns>
        public IEnumerable<Book> GetUnavailableBooks(IEnumerable<Book> allBooksWithTheSameName);

        /// <summary>
        /// Gets the books with the same title.
        /// </summary>
        /// <param name="title"> The title. </param>
        /// <returns> IEnumerable of Book. </returns>
        public IEnumerable<Book> GetBooksWithTheSameTitle(string title);
    }
}
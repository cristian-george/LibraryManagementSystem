// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Concretes namespace.
/// </summary>
namespace Library.DataLayer.Repository.Concretes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// Methods for the author controller.
    /// </summary>
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Gets the books with the same title.
        /// </summary>
        /// <param name="title"> The title. </param>
        /// <returns> IEnumerable of Book. </returns>
        public IEnumerable<Book> GetBooksWithTheSameTitle(string title)
        {
            try
            {
                return from book in this.Ctx.Books where book.Title == title select book;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "The query could not been made, will return empty list!");
            }

            return new List<Book>();
        }

        /// <summary>
        /// Gets the books with the same title.
        /// </summary>
        /// <param name="allBooksWithTheSameName"> The title. </param>
        /// <returns> IEnumerable of Book. </returns>
        public IEnumerable<Book> GetUnavailableBooks(IEnumerable<Book> allBooksWithTheSameName)
        {
            try
            {
                return from book in allBooksWithTheSameName where (bool)book.IsBorrowed || (bool)book.LecturesOnlyBook select book;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "The query could not been made, will return empty list!");
            }

            return new List<Book>();
        }
    }
}
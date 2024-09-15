// <copyright file="IBookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book service interface.
    /// Implements the <see cref="IService{Book}" />.
    /// </summary>
    /// <seealso cref="IService{Book}" />.
    public interface IBookService : IService<Book>
    {
        /// <summary>
        /// Checks if a book is in more than N domains.
        /// N is a threshold for number of domains.
        /// </summary>
        /// <param name="book">A Book.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        public bool IsInMoreThanNDomains(Book book);

        /// <summary>
        /// Checks if a book is in parent-child relation domains.
        /// </summary>
        /// <param name="book">A book.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        public bool IsInParentChildRelationDomains(Book book);
    }
}
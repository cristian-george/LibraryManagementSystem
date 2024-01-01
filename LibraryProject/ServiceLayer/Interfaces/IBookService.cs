// <copyright file="IBookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IBookService
    /// Implements the <see cref="IService{Book}" />.
    /// </summary>
    /// <seealso cref="IService{Book}" />.
    public interface IBookService : IService<Book>
    {
        /// <summary>
        /// Checks if a book belongs to at least one domain.
        /// </summary>
        /// <param name="book">A book entity.</param>
        /// <returns><c>true</c> if the book given as parameter belongs to
        /// at least one domain, <c>false</c> otherwise.</returns>
        bool IsInAtLeastOneDomain(Book book);

        /// <summary>
        /// Checks if a book belongs to the correct domains.
        /// </summary>
        /// <param name="book"> A book entity. </param>
        /// <returns><c>true</c> if the book given as parameter belongs to
        /// the correct domains, <c>false</c> otherwise.</returns>
        bool IsInTheCorrectDomains(Book book);

        /// <summary>
        /// Checks number of domains in which the book is classified.
        /// DOMENII represents a threshold value.
        /// </summary>
        /// <param name="book"> A book entity. </param>
        /// <returns> bool. </returns>
        bool IsInMoreThanDOMENIIDomains(Book book);
    }
}
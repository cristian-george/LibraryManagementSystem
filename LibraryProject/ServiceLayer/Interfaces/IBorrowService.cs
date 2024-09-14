// <copyright file="IBorrowService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Borrow service interface.
    /// Implements the <see cref="IService{Borrow}" />.
    /// </summary>
    /// <seealso cref="IService{Borrow}" />
    public interface IBorrowService : IService<Borrow>
    {
        /// <summary>
        /// Checks if at least one book is for lecture room.
        /// Checks if the number of books left is at least 10% from
        /// the initial fund of books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckCanBooksBeGranted(Borrow entity);

        /// <summary>
        /// Checks the can borrow maximum NMC in per.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowMaxNMCInPERMonths(Borrow entity);

        /// <summary>
        /// Checks the borrowed books for maximum c books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckBorrowedBooksForMaxCBooks(Borrow entity);

        /// <summary>
        /// Checks if borrow books are at most D in last L months.
        /// D is threshold for number of domains.
        /// L is threshold for number of months.
        /// </summary>
        /// <param name="entity">The borrow.</param>
        /// <returns>bool.</returns>
        bool CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(Borrow entity);

        /// <summary>
        /// Checks the lim.
        /// </summary>
        /// <param name="entity"> entity.</param>
        /// <returns> bool. </returns>
        bool CheckBorrowExtensionAtMostLIM(Borrow entity);

        /// <summary>
        /// Checks the borrow in delta time.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckBorrowsMadeInDELTADays(Borrow entity);

        /// <summary>
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowAtMostNCZBooksInOneDay(Borrow entity);

        /// <summary>
        /// Checks if librarian granted at most PERSIMP books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>bool.</returns>
        bool CheckGrantAtMostPERSIMPBooksInOneDay(Borrow entity);
    }
}
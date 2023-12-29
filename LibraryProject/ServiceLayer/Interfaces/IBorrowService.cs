// <copyright file="IBorrowService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IBorrowService
    /// Implements the <see cref="IService{Borrow}" />.
    /// </summary>
    /// <seealso cref="IService{Borrow}" />
    public interface IBorrowService : IService<Borrow>
    {
        /// <summary>
        /// Checks if books are borrowable.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckIfBooksAreBorrowable(Borrow entity);

        /// <summary>
        /// Checks the maximum borrow books today.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckMaxBorrowBooksToday();

        /// <summary>
        /// Checks the can borrow maximum NMC in per.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowMaxNMCInPER(Borrow entity);

        /// <summary>
        /// Checks the borrow in delta time.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckBorrowInDELTATime(Borrow entity);

        /// <summary>
        /// Checks the borrowed books for maximum c books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckBorrowedBooksForMaxCBooks(Borrow entity);
    }
}
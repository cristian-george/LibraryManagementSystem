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
        /// Checks if books can be borrowed. Each book must meet the following criteria:
        /// <para>1. Some copies of that book are marked as
        /// for reading room use only.</para>
        /// <para>2. The number of remaining books (those not borrowed yet,
        /// excluding those for reading room use) is at least 10% of the initial
        /// stock of that book.</para>
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckCanBooksBeGranted(Borrow entity);

        /// <summary>
        /// Checks if a reader can borrow maximum NMC books within a period of PER months.
        /// NMC is a threshold for the maximum number of books that can be borrowed.
        /// PER is a threshold for the number of months.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowMaxNMCInPERMonths(Borrow entity);

        /// <summary>
        /// Checks if a reader can take at most C books per borrow; if the number of books in a
        /// borrow request is at least 3, then every book must belong to at least 2 distinct domains.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckBorrowedBooksForMaxCBooks(Borrow entity);

        /// <summary>
        /// Checks if a reader can borrow more than D books from the same domain
        /// – either from a leaf type or a higher-level domain –
        /// in the last L months.
        /// D is threshold for number of domains.
        /// L is threshold for number of months.
        /// </summary>
        /// <param name="entity">The borrow.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowAtMostDBooksInSameDomainInLastLMonths(Borrow entity);

        /// <summary>
        /// Checks if a reader can borrow a book for a limited period;
        /// extensions are allowed, but the sum of these extensions
        /// granted in the last 3 months cannot exceed a given limit LIM.
        /// LIM is a threshold for the limit of books.
        /// </summary>
        /// <param name="entity"> entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckBorrowExtensionAtMostLIM(Borrow entity);

        /// <summary>
        /// Checks if a reader can borrow the same book
        /// multiple times within a period of DELTA days,
        /// where DELTA is measured from the last borrow of the book.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckBorrowsMadeInDELTADays(Borrow entity);

        /// <summary>
        /// Checks if a reader can borrow at most NCZ books in a day;
        /// this threshold is ignored for library staff.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckCanBorrowAtMostNCZBooksInOneDay(Borrow entity);

        /// <summary>
        /// Checks if librarian granted at most PERSIMP books today.
        /// PERSIMP is a threshold for the number of books.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if the check succeed, <c>false</c> otherwise.</returns>
        bool CheckGrantAtMostPERSIMPBooksInOneDay(Borrow entity);
    }
}
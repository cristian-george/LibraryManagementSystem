// <copyright file="LibrarianRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Concretes namespace.
/// </summary>
namespace Library.DataLayer.Repository.Concretes
{
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// Class LibrarianRepository.
    /// Implements the <see cref="BaseRepository{DomainLayer.Person.Librarian}" />.
    /// Implements the <see cref="ILibrarianRepository" />.
    /// </summary>
    /// <seealso cref="BaseRepository{DomainLayer.Person.Librarian}" />.
    /// <seealso cref="ILibrarianRepository" />.
    public class LibrarianRepository : BaseRepository<Librarian>, ILibrarianRepository
    {
    }
}
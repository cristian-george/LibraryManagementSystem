// <copyright file="ILibrarianRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the librarian controller.
    /// </summary>
    public interface ILibrarianRepository : IRepository<Librarian>
    {
    }
}
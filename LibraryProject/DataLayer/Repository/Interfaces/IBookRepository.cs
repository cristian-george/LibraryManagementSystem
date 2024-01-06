// <copyright file="IBookRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the book controller.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
    }
}
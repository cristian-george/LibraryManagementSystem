// <copyright file="IReaderRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the reader controller.
    /// </summary>
    public interface IReaderRepository : IRepository<Reader>
    {
        // public Reader GetByIDWithInclude(object id, string includeProperties = "");
    }
}
// <copyright file="ReaderRepository.cs" company="Transilvania University of Brasov">
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
    /// Methods for the reader controller.
    /// </summary>
    public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
    {
    }
}
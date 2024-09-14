// <copyright file="EditionRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition repository.
    /// </summary>
    public class EditionRepository : BaseRepository<Edition>, IEditionRepository
    {
    }
}
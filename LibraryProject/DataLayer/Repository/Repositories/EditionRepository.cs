// <copyright file="EditionRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition repository.
    /// </summary>
    public class EditionRepository : BaseRepository<Edition>, IEditionRepository
    {
    }
}
// <copyright file="AuthorRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repositories
{
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Author repository.
    /// </summary>
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
    }
}
// <copyright file="IAuthorRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Interfaces
{
    using Library.DataLayer;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Author repository interface.
    /// </summary>
    public interface IAuthorRepository : IRepository<Author>
    {
    }
}
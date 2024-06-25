// <copyright file="IUserRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// User repository interface.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
    }
}
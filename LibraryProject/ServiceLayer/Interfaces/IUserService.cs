// <copyright file="IUserService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// User service interface.
    /// Implements the <see cref="IService{User}" />.
    /// </summary>
    /// <seealso cref="IService{User}" />
    public interface IUserService : IService<User>
    {
    }
}
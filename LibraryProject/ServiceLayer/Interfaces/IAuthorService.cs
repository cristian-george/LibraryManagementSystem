// <copyright file="IAuthorService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Author service interface.
    /// Implements the <see cref="IService{Author}" />.
    /// </summary>
    /// <seealso cref="IService{Author}" />
    public interface IAuthorService : IService<Author>
    {
    }
}
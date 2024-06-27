// <copyright file="IEditionService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition service interface.
    /// Implements the <see cref="IService{Edition}" />.
    /// </summary>
    /// <seealso cref="IService{Edition}" />
    public interface IEditionService : IService<Edition>
    {
    }
}
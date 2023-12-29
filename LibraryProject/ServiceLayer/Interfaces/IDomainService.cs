// <copyright file="IDomainService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IDomainService
    /// Implements the <see cref="IService{Domain}" />.
    /// </summary>
    /// <seealso cref="IService{Domain}" />
    public interface IDomainService : IService<Domain>
    {
    }
}
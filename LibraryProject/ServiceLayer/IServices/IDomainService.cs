// <copyright file="IDomainService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IDomainService
    /// Implements the <see cref="IBaseService{Domain}" />.
    /// </summary>
    /// <seealso cref="IBaseService{Domain}" />
    public interface IDomainService : IBaseService<Domain>
    {
    }
}
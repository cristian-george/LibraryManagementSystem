// <copyright file="IEditionService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IEditionService
    /// Implements the <see cref="IBaseService{Edition}" />.
    /// </summary>
    /// <seealso cref="IBaseService{Edition}" />
    public interface IEditionService : IBaseService<Edition>
    {
    }
}
// <copyright file="IAuthorService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IAuthorService
    /// Implements the <see cref="IBaseService{Author}" />.
    /// </summary>
    /// <seealso cref="IBaseService{Author}" />
    public interface IAuthorService : IBaseService<Author>
    {
    }
}
// <copyright file="IBookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IBookService
    /// Implements the <see cref="IBaseService{Book}" />.
    /// </summary>
    /// <seealso cref="IBaseService{Book}" />.
    public interface IBookService : IBaseService<Book>
    {
    }
}
// <copyright file="IBookService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IBookService
    /// Implements the <see cref="IService{Book}" />.
    /// </summary>
    /// <seealso cref="IService{Book}" />.
    public interface IBookService : IService<Book>
    {
    }
}
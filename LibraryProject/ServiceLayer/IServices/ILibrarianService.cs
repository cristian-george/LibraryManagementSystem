// <copyright file="ILibrarianService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface ILibrarianService
    /// Implements the <see cref="IBaseService{Library.DomainLayer.Person.Librarian}" />.
    /// </summary>
    /// <seealso cref="IBaseService{Library.DomainLayer.Person.Librarian}" />
    public interface ILibrarianService : IBaseService<Librarian>
    {
    }
}
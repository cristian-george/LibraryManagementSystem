// <copyright file="ILibrarianService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface ILibrarianService
    /// Implements the <see cref="IService{Library.DomainLayer.Person.Librarian}" />.
    /// </summary>
    /// <seealso cref="IService{Library.DomainLayer.Person.Librarian}" />
    public interface ILibrarianService : IService<Librarian>
    {
    }
}
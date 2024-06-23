// <copyright file="IReaderService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IReaderService
    /// Implements the <see cref="IService{DomainLayer.Person.Reader}" />.
    /// </summary>
    /// <seealso cref="IService{DomainLayer.Person.Reader}" />
    public interface IReaderService : IService<Reader>
    {
    }
}
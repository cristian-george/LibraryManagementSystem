// <copyright file="IBorrowerService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IBorrowerService
    /// Implements the <see cref="IService{DomainLayer.Person.Borrower}" />.
    /// </summary>
    /// <seealso cref="IService{DomainLayer.Person.Borrower}" />
    public interface IBorrowerService : IService<Borrower>
    {
    }
}
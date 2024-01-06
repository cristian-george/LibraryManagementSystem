// <copyright file="IAccountService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IAccountService
    /// Implements the <see cref="IService{DomainLayer.Person.Account}"/>.
    /// </summary>
    /// <seealso cref="IService{DomainLayer.Person.Account}" />
    public interface IAccountService : IService<Account>
    {
    }
}
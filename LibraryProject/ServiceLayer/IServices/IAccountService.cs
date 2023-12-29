// <copyright file="IAccountService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.IServices
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IAccountService
    /// Implements the <see cref="IBaseService{Library.DomainLayer.Person.Account}"/>.
    /// </summary>
    /// <seealso cref="IBaseService{Library.DomainLayer.Person.Account}" />
    public interface IAccountService : IBaseService<Account>
    {
    }
}
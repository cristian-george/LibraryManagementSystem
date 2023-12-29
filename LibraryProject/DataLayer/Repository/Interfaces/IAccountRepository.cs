// <copyright file="IAccountRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface IAccountRepository.
    /// Implements the <see cref="IRepository{Library.DomainLayer.Person.Account}" />.
    /// </summary>
    /// <seealso cref="IRepository{Library.DomainLayer.Person.Account}" />
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
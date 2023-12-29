// <copyright file="AccountRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Concretes namespace.
/// </summary>
namespace Library.DataLayer.Repository.Concretes
{
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// The Concretes namespace.
    /// <seealso cref="IAccountRepository"/>
    /// </summary>
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
    }
}
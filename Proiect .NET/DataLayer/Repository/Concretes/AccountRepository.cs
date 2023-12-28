// <copyright file="AccountRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Concretes namespace.
/// </summary>
namespace Library.DataLayer.Concretes
{
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer;

    /// <summary>
    /// The Concretes namespace.
    /// <seealso cref="Library.DataLayer.Interfaces.IAccountRepository"/>
    /// </summary>
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
    }
}
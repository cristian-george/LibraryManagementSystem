// <copyright file="IDomainRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the domain controller.
    /// </summary>
    public interface IDomainRepository : IRepository<Domain>
    {
    }
}
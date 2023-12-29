// <copyright file="IBorrowerRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer;

    /// <summary>
    /// Interface for the borrower controller.
    /// </summary>
    public interface IBorrowerRepository : IRepository<Borrower>
    {
        // public Borrower GetByIDWithInclude(object id, string includeProperties = "");
    }
}
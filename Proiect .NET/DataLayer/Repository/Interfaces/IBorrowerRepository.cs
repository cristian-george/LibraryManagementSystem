// <copyright file="IBorrowerRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Interfaces
{
    using Library.DomainLayer.Person;

    /// <summary>
    /// Interface for the borrower controller.
    /// </summary>
    public interface IBorrowerRepository : IRepository<Borrower>
    {
        // public Borrower GetByIDWithInclude(object id, string includeProperties = "");
    }
}
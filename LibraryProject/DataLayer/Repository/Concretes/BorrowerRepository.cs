// <copyright file="BorrowerRepository.cs" company="Transilvania University of Brasov">
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
    /// Methods for the borrower controller.
    /// </summary>
    public class BorrowerRepository : BaseRepository<Borrower>, IBorrowerRepository
    {
    }
}
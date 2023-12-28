// <copyright file="BorrowerRepository.cs" company="Transilvania University of Brasov">
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
    /// Methods for the borrower controller.
    /// </summary>
    public class BorrowerRepository : BaseRepository<Borrower>, IBorrowerRepository
    {
    }
}
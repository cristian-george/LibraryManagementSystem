// <copyright file="IStockRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock repository interface.
    /// </summary>
    public interface IStockRepository : IRepository<Stock>
    {
    }
}
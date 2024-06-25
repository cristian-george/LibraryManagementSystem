// <copyright file="StockRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Repository.Concretes
{
    using Library.DataLayer.Repository;
    using Library.DataLayer.Repository.Interfaces;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock repository.
    /// </summary>
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
    }
}
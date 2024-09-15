// <copyright file="IStockService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Interfaces
{
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock service interface.
    /// Implements the <see cref="IService{Stock}"/>.
    /// </summary>
    /// <seealso cref="IService{Stock}" />
    public interface IStockService : IService<Stock>
    {
    }
}
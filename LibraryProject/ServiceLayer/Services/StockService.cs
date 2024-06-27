// <copyright file="StockService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class StockService.
    /// Implements the <see cref="Services.BaseService{DomainLayer.Person.Account, IAccountRepository}" />
    /// Implements the <see cref="IStockService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{DomainLayer.Person.Account, IAccountRepository}" />
    /// <seealso cref="IStockService" />
    public class StockService : BaseService<Stock, IStockRepository>, IStockService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockService" /> class.
        /// </summary>
        public StockService()
            : base(Injector.Create<IStockRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new StockValidator();
        }
    }
}
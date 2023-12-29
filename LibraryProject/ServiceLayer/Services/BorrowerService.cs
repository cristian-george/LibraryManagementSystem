// <copyright file="BorrowerService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer.IServices;

    /// <summary>
    /// Class BorrowerService.
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Library.DomainLayer.Person.Borrower, IBorrowerRepository}" />
    /// Implements the <see cref="IBorrowerService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Library.DomainLayer.Person.Borrower, IBorrowerRepository}" />
    /// <seealso cref="IBorrowerService" />
    public class BorrowerService : BaseService<Borrower, IBorrowerRepository, IPropertiesRepository>, IBorrowerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerService" /> class.
        /// </summary>
        public BorrowerService()
            : base(Injector.Create<IBorrowerRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new BorrowerValidator();
        }
    }
}
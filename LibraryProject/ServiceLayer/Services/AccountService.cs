// <copyright file="AccountService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class AccountService.
    /// Implements the <see cref="Services.BaseService{DomainLayer.Person.Account, IAccountRepository}" />
    /// Implements the <see cref="IAccountService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{DomainLayer.Person.Account, IAccountRepository}" />
    /// <seealso cref="IAccountService" />
    public class AccountService : BaseService<Account, IAccountRepository>, IAccountService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        public AccountService()
            : base(Injector.Create<IAccountRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new AccountValidator();
        }
    }
}
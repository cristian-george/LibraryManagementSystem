// <copyright file="UserService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class UserService.
    /// Implements the <see cref="Services.BaseService{User, IUserRepository}" />
    /// Implements the <see cref="IUserService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{User, IUserRepository}" />
    /// <seealso cref="IUserService" />
    public class UserService : BaseService<User, IUserRepository>, IUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        public UserService()
            : base(Injector.Create<IUserRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new UserValidator();
        }
    }
}
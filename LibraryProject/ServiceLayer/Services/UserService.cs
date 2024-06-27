// <copyright file="UserService.cs" company="Transilvania University of Brasov">
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
    /// Class UserService.
    /// Implements the <see cref="Services.BaseService{DomainLayer.Person.Reader, IReaderRepository}" />
    /// Implements the <see cref="IUserService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{DomainLayer.Person.Reader, IReaderRepository}" />
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
﻿// <copyright file="AuthorService.cs" company="Transilvania University of Brasov">
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
    /// Class AuthorService.
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Author, IAuthorRepository}" />
    /// Implements the <see cref="IAuthorService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Author, IAuthorRepository}" />
    /// <seealso cref="IAuthorService" />
    public class AuthorService : BaseService<Author, IAuthorRepository, IPropertiesRepository>, IAuthorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService" /> class.
        /// </summary>
        public AuthorService()
            : base(Injector.Create<IAuthorRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new AuthorValidator();
        }
    }
}
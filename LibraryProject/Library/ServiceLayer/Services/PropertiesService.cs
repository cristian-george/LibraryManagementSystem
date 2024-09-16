// <copyright file="PropertiesService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentValidation;
    using FluentValidation.Results;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class PropertiesService.
    /// Implements the <see cref="IPropertiesService" />.
    /// </summary>
    /// <seealso cref="IPropertiesService" />
    public class PropertiesService : IPropertiesService
    {
        /// <summary>
        /// The validator.
        /// </summary>
        private readonly IValidator<Properties> validator;

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IPropertiesRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesService" /> class.
        /// </summary>
        public PropertiesService()
        {
            this.repository = Injector.Create<IPropertiesRepository>();
            this.validator = new PropertiesValidator();
        }

        /// <inheritdoc/>
        public bool Insert(Properties entity)
        {
            var result = this.validator.Validate(entity);
            if (result.IsValid)
            {
                _ = this.repository.Insert(entity);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public Properties GetById(object id)
        {
            return this.repository.GetById(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Properties> Get(
            Expression<Func<Properties, bool>> filter = null,
            Func<IQueryable<Properties>, IOrderedQueryable<Properties>> orderBy = null)
        {
            return this.repository.Get(filter, book => book.OrderBy(x => x.Id));
        }

        /// <inheritdoc/>
        public bool Update(Properties entity)
        {
            var context = new ValidationContext<Properties>(entity);
            ValidationResult result = this.validator.Validate(context);
            if (result.IsValid)
            {
                _ = this.repository.Update(entity);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool DeleteById(object id)
        {
            return this.repository.DeleteById(id);
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            return this.repository.Delete();
        }
    }
}
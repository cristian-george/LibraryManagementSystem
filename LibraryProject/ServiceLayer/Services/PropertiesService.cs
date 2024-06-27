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
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer.Models;
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
        private readonly IValidator validator;

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

        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> ceva. </returns>
        public bool Delete()
        {
            return this.repository.Delete();
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DeleteById(object entity)
        {
            return this.repository.DeleteById(entity);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns> ceva. </returns>
        public IEnumerable<Properties> Get(
            Expression<Func<Properties, bool>> filter = null,
            Func<IQueryable<Properties>, IOrderedQueryable<Properties>> orderBy = null,
            string includeProperties = "")
        {
            return this.repository.Get(filter, book => book.OrderBy(x => x.Id), includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> ceva. </returns>
        public Properties GetById(object id)
        {
            return this.repository.GetById(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> ceva. </returns>
        public bool Insert(Properties entity)
        {
            var context = new ValidationContext<Properties>(entity);
            ValidationResult result = this.validator.Validate(context);
            if (result.IsValid)
            {
                _ = this.repository.Insert(entity);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> ceva. </returns>
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
    }
}
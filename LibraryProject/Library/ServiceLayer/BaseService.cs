// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentValidation;
    using Library.DataLayer;
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Interfaces;

    /// <summary>
    /// Class BaseService.
    /// Implements the <see cref="IService{T}"/>.
    /// </summary>
    /// <typeparam name="TModel"> Reference type. </typeparam>
    /// <typeparam name="TRepository">Implements IRepository of TModel.</typeparam>
    /// <seealso cref="IService{T}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BaseService{TModel, TRepository}"/> class.
    /// </remarks>
    /// <param name="repository">The repository.</param>
    /// <param name="propertiesRepository">The properties repository.</param>
    public abstract class BaseService<TModel, TRepository>
        (TRepository repository, IPropertiesRepository propertiesRepository)
        : IService<TModel>
        where TModel : class, IEntity
        where TRepository : IRepository<TModel>
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        protected TRepository Repository { get; } = repository;

        /// <summary>
        /// Gets the properties repository.
        /// </summary>
        /// <value>The properties repository.</value>
        protected IPropertiesRepository PropertiesRepository { get; } = propertiesRepository;

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value>The validator.</value>
        protected IValidator<TModel> Validator { get; set; }

        /// <inheritdoc/>
        public virtual bool Insert(TModel entity)
        {
            var result = this.Validator.Validate(entity);

            if (!result.IsValid)
            {
                Logging.LogErrors(result);
                return false;
            }

            _ = this.Repository.Insert(entity);
            return true;
        }

        /// <inheritdoc/>
        public virtual bool Update(TModel entity)
        {
            var result = this.Validator.Validate(entity);

            if (!result.IsValid)
            {
                Logging.LogErrors(result);
                return false;
            }

            _ = this.Repository.Update(entity);
            return true;
        }

        /// <inheritdoc/>
        public virtual bool DeleteById(object id)
        {
            return this.Repository.DeleteById(id);
        }

        /// <inheritdoc/>
        public virtual TModel GetById(object id)
        {
            return this.Repository.GetById(id);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "")
        {
            return this.Repository.Get(filter, orderBy, includeProperties);
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            return this.Repository.Delete();
        }
    }
}
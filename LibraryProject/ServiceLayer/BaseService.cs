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

    /// <summary>
    /// Class BaseService.
    /// Implements the <see cref="IService{T}" />.
    /// </summary>
    /// <typeparam name="TModel"> Reference type. </typeparam>
    /// <typeparam name="TRepository"> Implements IRepository of TModel. </typeparam>
    /// <seealso cref="IService{T}" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="BaseService{TModel, TRepository}"/> class.
    /// </remarks>
    /// <param name="repository"> The repository. </param>
    /// <param name="propertiesRepository"> The property repo. </param>
    public abstract class BaseService<TModel, TRepository>
        (TRepository repository, IPropertiesRepository propertiesRepository)
        : IService<TModel>
        where TModel : class
        where TRepository : IRepository<TModel>
    {
        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value> The repository. </value>
        protected TRepository Repository { get; set; } = repository;

        /// <summary>
        /// Gets or sets the properties repository.
        /// </summary>
        /// <value> The properties repository. </value>
        protected IPropertiesRepository PropertiesRepository { get; set; } = propertiesRepository;

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value> The validator. </value>
        protected IValidator<TModel> Validator { get; set; }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> None. </returns>
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

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>
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

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> bool. </returns>
        public virtual bool DeleteById(object id)
        {
            return this.Repository.DeleteById(id);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> TModel. </returns>
        public virtual TModel GetById(object id)
        {
            return this.Repository.GetById(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns> IEnumerable of TModel. </returns>
        public virtual IEnumerable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "")
        {
            return this.Repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <returns> bool. </returns>
        public bool Delete()
        {
            return this.Repository.Delete();
        }
    }
}
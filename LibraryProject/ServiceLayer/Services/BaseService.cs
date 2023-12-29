// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentValidation;
    using Library.DataLayer.Repository.Interfaces;
    using Library.ServiceLayer;
    using Library.ServiceLayer.IServices;

    /// <summary>
    /// Class BaseService.
    /// Implements the <see cref="IBaseService{T}" />.
    /// </summary>
    /// <typeparam name="TModel"> Reference type. </typeparam>
    /// <typeparam name="TRepository"> Implements IRepository. <TModel>. </typeparam>
    /// <typeparam name="TPropRepository"> Implements IPropertiesRepository. </typeparam>
    /// <seealso cref="IBaseService{T}" />
    public abstract class BaseService<TModel, TRepository, TPropRepository> : IBaseService<TModel>
        where TModel : class
        where TRepository : IRepository<TModel>
        where TPropRepository : IPropertiesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TModel, TRepository, TPropRepository}"/> class.
        /// </summary>
        /// <param name="repository"> The repository. </param>
        /// <param name="propRepo"> The property repo. </param>
        protected BaseService(TRepository repository, TPropRepository propRepo)
        {
            this.Repository = repository;
            this.PropertiesRepository = propRepo;
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value> The repository. </value>
        protected TRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value> The validator. </value>
        protected IValidator<TModel> Validator { get; set; }

        /// <summary>
        /// Gets or sets the properties repository.
        /// </summary>
        /// <value> The properties repository. </value>
        protected TPropRepository PropertiesRepository { get; set; }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> None. </returns>
        public virtual bool Insert(TModel entity)
        {
            var result = this.Validator.Validate(entity);
            var isValid = false;
            if (result.IsValid)
            {
                isValid = true;
            }
            else
            {
                Utils.LogErrors(result);
                return false;
            }

            if (isValid == true)
            {
                this.Repository.Insert(entity);
            }

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
            Utils.LogErrors(result);
            if (result.IsValid)
            {
                this.Repository.Update(entity);
            }
            else
            {
                Utils.LogErrors(result);
                return false;
            }

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
        public virtual TModel GetByID(object id)
        {
            return this.Repository.GetByID(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns> IEnumerable of TModel. </returns>
        public virtual IEnumerable<TModel> GetAll(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "")
        {
            return this.Repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>
        public bool DeleteAll()
        {
            return this.Repository.DeleteAllEntitiesFromTable();
        }
    }
}
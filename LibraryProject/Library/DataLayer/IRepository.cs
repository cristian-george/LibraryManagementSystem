// <copyright file="IRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Library.DomainLayer.Interfaces;

    /// <summary>
    /// Interface for the repository.
    /// </summary>
    /// <typeparam name="TModel">A reference type that implements IEntity.</typeparam>
    public interface IRepository<TModel>
        where TModel : class, IEntity
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>.
        bool Insert(TModel entity);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Object of type TModel.</returns>.
        TModel GetById(object id);

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable of TModel.</returns>.
        IEnumerable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>.
        bool Update(TModel entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Bool.</returns>.
        bool DeleteById(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>.
        bool Delete(TModel entity);

        /// <summary>
        /// Deletes all entities from table.
        /// </summary>
        /// <returns>Bool.</returns>.
        bool Delete();
    }
}
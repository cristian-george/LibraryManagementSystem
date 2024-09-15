// <copyright file="IService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for the service.
    /// </summary>
    /// <typeparam name="T"> A reference type. </typeparam>
    public interface IService<T>
        where T : class
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> TModel. </returns>
        bool Insert(T entity);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>
        bool Update(T entity);

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns>Bool.</returns>
        bool DeleteById(object id);

        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>
        bool Delete();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> Object of type <typeparamref name="T"/>. </returns>
        T GetById(object id);

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter"> filter. </param>
        /// <param name="orderBy"> orderBy. </param>
        /// <param name="includeProperties"> includeProperties. </param>
        /// <returns> IEnumerable of type <typeparamref name="T"/>. </returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}
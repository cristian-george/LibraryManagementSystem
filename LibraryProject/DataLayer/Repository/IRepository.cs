// <copyright file="IRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Interfaces namespace.
/// </summary>
namespace Library.DataLayer.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for the repository.
    /// </summary>
    /// <typeparam name="T"> A reference type. </typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>.
        bool Insert(T entity);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> bool. </returns>.
        bool Update(T item);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> bool. </returns>.
        bool DeleteById(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> bool. </returns>.
        bool Delete(T entity);

        /// <summary>
        /// Deletes all entities from table.
        /// </summary>
        /// <returns> bool. </returns>.
        bool DeleteAllEntitiesFromTable();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> Object of type T. </returns>.
        T GetByID(object id);

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns> IEnumerable of T. </returns>.
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
    }
}
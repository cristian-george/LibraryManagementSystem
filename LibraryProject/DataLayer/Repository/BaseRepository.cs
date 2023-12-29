// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The DataLayer namespace.
/// </summary>
namespace Library.DataLayer.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Library.DataLayer;
    using Library.DataLayer.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using NLog;

    /// <summary>
    /// Abstract class to be inherited to implement the CRUD operation for an entity.
    /// </summary>
    /// <typeparam name="T"> Type of the controller. </typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets the CTX.
        /// </summary>
        /// <value> The CTX. </value>
        protected LibraryContext Ctx { get; } = new LibraryContext();

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value> The logger. </value>
        protected Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns> IEnumerable of T. </returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                var databaseSet = this.Ctx.Set<T>();

                IQueryable<T> query = databaseSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "The query will return an empty list!");
            }

            return null;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>
        public virtual bool Insert(T entity)
        {
            try
            {
                var databaseSet = this.Ctx.Set<T>();
                _ = databaseSet.Add(entity);
                _ = this.Ctx.SaveChanges();

                entity = null;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The INSERT could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item"> The item. </param>
        /// <returns> bool. </returns>
        public virtual bool Update(T item)
        {
            try
            {
                var databaseSet = this.Ctx.Set<T>();
                _ = databaseSet.Attach(item);
                this.Ctx.Entry(item).State = EntityState.Modified;

                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The UPDATE could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> bool. </returns>
        public virtual bool DeleteById(object id)
        {
            try
            {
                _ = this.Delete(this.GetByID(id));
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The DELETE could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete"> The entity to delete. </param>
        /// <returns> bool. </returns>
        public virtual bool Delete(T entityToDelete)
        {
            try
            {
                var dbSet = this.Ctx.Set<T>();

                if (this.Ctx.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _ = dbSet.Attach(entityToDelete);
                }

                _ = dbSet.Remove(entityToDelete);

                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The DELETE could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id"> The identifier. </param>
        /// <returns> Object of type T. </returns>
        public virtual T GetByID(object id)
        {
            try
            {
                return this.Ctx.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The GetByID could not been made. Will return null!");
            }

            return null;
        }

        /// <summary>
        /// Deletes all entities from table.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> bool. </returns>
        public bool DeleteAllEntitiesFromTable()
        {
            try
            {
                var dbSet = this.Ctx.Set<T>();
                dbSet.RemoveRange(dbSet);
                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The DeleteAllEntitiesFromTable could not been made.");
                return false;
            }

            return true;
        }
    }
}
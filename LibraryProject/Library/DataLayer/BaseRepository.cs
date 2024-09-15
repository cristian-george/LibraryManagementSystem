// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Library.DomainLayer.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using NLog;

    /// <summary>
    /// Abstract class to be inherited to implement the CRUD operation for an entity.
    /// </summary>
    /// <typeparam name="TModel">Type of the controller.</typeparam>
    public abstract class BaseRepository<TModel> : IRepository<TModel>
        where TModel : class, IEntity
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        protected LibraryDbContext Ctx { get; } = new LibraryDbContext();

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>
        public virtual bool Insert(TModel entity)
        {
            try
            {
                var databaseSet = this.Ctx.Set<TModel>();
                _ = this.Ctx.Attach(entity);
                _ = databaseSet.Add(entity);
                _ = this.Ctx.SaveChanges();

                entity = null;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Insert could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Object of type TModel.</returns>
        public virtual TModel GetById(object id)
        {
            try
            {
                return this.Ctx.Set<TModel>().Find(id);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The GetById could not been made. Will return null!");
            }

            return null;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter"> The filter. </param>
        /// <param name="orderBy"> The order by. </param>
        /// <param name="includeProperties"> The include properties. </param>
        /// <returns>IEnumerable of TModel.</returns>
        public virtual IEnumerable<TModel> Get(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                var databaseSet = this.Ctx.Set<TModel>();

                IQueryable<TModel> query = databaseSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
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
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Bool.</returns>
        public virtual bool Update(TModel entity)
        {
            try
            {
                var databaseSet = this.Ctx.Set<TModel>();
                var trackedEntity = this.Ctx.ChangeTracker.Entries<TModel>().FirstOrDefault(e => e.Entity.Id == entity.Id);

                if (trackedEntity != null)
                {
                    this.Ctx.Entry(trackedEntity.Entity).CurrentValues.SetValues(entity);
                }
                else
                {
                    _ = databaseSet.Attach(entity);
                    this.Ctx.Entry(entity).State = EntityState.Modified;
                }

                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Update could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Bool.</returns>
        public virtual bool DeleteById(object id)
        {
            try
            {
                _ = this.Delete(this.GetById(id));
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The DeleteById could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>Bool.</returns>
        public virtual bool Delete(TModel entity)
        {
            try
            {
                var dbSet = this.Ctx.Set<TModel>();

                if (this.Ctx.Entry(entity).State == EntityState.Detached)
                {
                    _ = dbSet.Attach(entity);
                }

                _ = dbSet.Remove(entity);

                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Delete could not been made!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes all entities from table.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool Delete()
        {
            try
            {
                var dbSet = this.Ctx.Set<TModel>();
                dbSet.RemoveRange(dbSet);
                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Delete could not been made.");
                return false;
            }

            return true;
        }
    }
}
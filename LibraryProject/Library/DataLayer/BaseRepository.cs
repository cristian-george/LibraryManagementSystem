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
    /// Base class to be inherited for specializing the CRUD operations for an entity.
    /// </summary>
    /// <typeparam name="TModel">Type of the controller.</typeparam>
    public class BaseRepository<TModel> : IRepository<TModel>
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

        /// <inheritdoc/>
        public virtual bool Insert(TModel entity)
        {
            try
            {
                var databaseSet = this.Ctx.Set<TModel>();
                _ = this.Ctx.Attach(entity);
                _ = databaseSet.Add(entity);
                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Insert could not been made!");
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public virtual TModel GetById(object id)
        {
            return this.Ctx.Set<TModel>().Find(id);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TModel> Get(
            Expression<Func<TModel, bool>> filterBy = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "")
        {
            var databaseSet = this.Ctx.Set<TModel>();

            IQueryable<TModel> query = databaseSet;

            if (filterBy != null)
            {
                query = query.Where(filterBy);
            }

            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        /// <inheritdoc/>
        public virtual bool Update(TModel entity)
        {
            var databaseSet = this.Ctx.Set<TModel>();
            try
            {
                _ = databaseSet.Attach(entity);
                this.Ctx.Entry(entity).State = EntityState.Modified;
                _ = this.Ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.InnerException + "The Update could not been made!");
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public virtual bool DeleteById(object id)
        {
            return this.Delete(this.GetById(id));
        }

        /// <inheritdoc/>
        public virtual bool Delete(TModel entity)
        {
            try
            {
                var dbSet = this.Ctx.Set<TModel>();
                _ = dbSet.Attach(entity);
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

        /// <inheritdoc/>
        public bool Delete()
        {
            var dbSet = this.Ctx.Set<TModel>();
            dbSet.RemoveRange(dbSet);
            _ = this.Ctx.SaveChanges();

            return true;
        }
    }
}
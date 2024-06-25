// <copyright file="Bindings.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.Injection
{
    using Library.DataLayer.Repository.Concretes;
    using Library.DataLayer.Repository.Interfaces;
    using Library.ServiceLayer.Interfaces;
    using Library.ServiceLayer.Services;
    using Ninject.Modules;

    /// <summary>
    /// Class Bindings.
    /// Implements the <see cref="NinjectModule" />.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class Bindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.LoadRepositoryLayer();
            this.LoadServiceLayer();
        }

        /// <summary>
        /// Loads the repository layer.
        /// </summary>
        private void LoadRepositoryLayer()
        {
            _ = this.Bind<IAuthorRepository>().To<AuthorRepository>();
            _ = this.Bind<IBookRepository>().To<BookRepository>();
            _ = this.Bind<IBorrowRepository>().To<BorrowRepository>();
            _ = this.Bind<IDomainRepository>().To<DomainRepository>();
            _ = this.Bind<IEditionRepository>().To<EditionRepository>();
            _ = this.Bind<IPropertiesRepository>().To<PropertiesRepository>();
            _ = this.Bind<IStockRepository>().To<StockRepository>();
            _ = this.Bind<IUserRepository>().To<UserRepository>();
        }

        /// <summary>
        /// Loads the service layer.
        /// </summary>
        private void LoadServiceLayer()
        {
            _ = this.Bind<IAuthorService>().To<AuthorService>();
            _ = this.Bind<IBookService>().To<BookService>();
            _ = this.Bind<IBorrowService>().To<BorrowService>();
            _ = this.Bind<IDomainService>().To<DomainService>();
            _ = this.Bind<IEditionService>().To<EditionService>();
            _ = this.Bind<IPropertiesService>().To<PropertiesService>();
            _ = this.Bind<IStockService>().To<StockService>();
            _ = this.Bind<IUserService>().To<UserService>();
        }
    }
}
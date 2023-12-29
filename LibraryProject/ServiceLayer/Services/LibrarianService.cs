// <copyright file="LibrarianService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class LibrarianService.
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Library.DomainLayer.Person.Librarian, ILibrarianRepository}" />
    /// Implements the <see cref="ILibrarianService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Library.DomainLayer.Person.Librarian, ILibrarianRepository}" />
    /// <seealso cref="ILibrarianService" />
    public class LibrarianService : BaseService<Librarian, ILibrarianRepository, IPropertiesRepository>, ILibrarianService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianService" /> class.
        /// </summary>
        public LibrarianService()
            : base(Injector.Create<ILibrarianRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new LibrarianValidator();
        }
    }
}
// <copyright file="ReaderService.cs" company="Transilvania University of Brasov">
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
    /// Class ReaderService.
    /// Implements the <see cref="Services.BaseService{DomainLayer.Person.Reader, IReaderRepository}" />
    /// Implements the <see cref="IReaderService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{DomainLayer.Person.Reader, IReaderRepository}" />
    /// <seealso cref="IReaderService" />
    public class ReaderService : BaseService<Reader, IReaderRepository>, IReaderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService" /> class.
        /// </summary>
        public ReaderService()
            : base(Injector.Create<IReaderRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new ReaderValidator();
        }
    }
}
// <copyright file="EditionService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
    using Library.Injection;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class EditionService.
    /// Implements the <see cref="Services.BaseService{Edition, IEditionRepository}" />
    /// Implements the <see cref="IEditionService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{Edition, IEditionRepository}" />
    /// <seealso cref="IEditionService" />
    public class EditionService : BaseService<Edition, IEditionRepository>, IEditionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionService" /> class.
        /// </summary>
        public EditionService()
            : base(Injector.Create<IEditionRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new EditionValidator();
        }
    }
}
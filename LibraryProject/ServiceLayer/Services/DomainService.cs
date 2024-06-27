// <copyright file="DomainService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.Interfaces;

    /// <summary>
    /// Class DomainService.
    /// Implements the <see cref="Services.BaseService{Domain, IDomainRepository}" />
    /// Implements the <see cref="IDomainService" />.
    /// </summary>
    /// <seealso cref="Services.BaseService{Domain, IDomainRepository}" />
    /// <seealso cref="IDomainService" />
    public class DomainService : BaseService<Domain, IDomainRepository>, IDomainService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainService" /> class.
        /// </summary>
        public DomainService()
             : base(Injector.Create<IDomainRepository>(), Injector.Create<IPropertiesRepository>())
        {
            this.Validator = new DomainValidator();
        }

        /// <inheritdoc/>
        public override bool Insert(Domain entity)
        {
            var result = this.Validator.Validate(entity);

            if (!result.IsValid)
            {
                LogUtils.LogErrors(result);
                return false;
            }

            entity.SetParentDomain();

            _ = this.Repository.Insert(entity);
            return true;
        }
    }
}
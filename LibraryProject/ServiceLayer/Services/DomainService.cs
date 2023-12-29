// <copyright file="DomainService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Repository.Interfaces;
    using Library.DataLayer.Validators.DomainValidators;
    using Library.DomainLayer;
    using Library.Injection;
    using Library.ServiceLayer;
    using Library.ServiceLayer.IServices;

    /// <summary>
    /// Class DomainService.
    /// Implements the <see cref="Library.ServiceLayer.Services.BaseService{Domain, IDomainRepository}" />
    /// Implements the <see cref="IDomainService" />.
    /// </summary>
    /// <seealso cref="Library.ServiceLayer.Services.BaseService{Domain, IDomainRepository}" />
    /// <seealso cref="IDomainService" />
    public class DomainService : BaseService<Domain, IDomainRepository, IPropertiesRepository>, IDomainService
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
            if (entity.ParentDomain == null)
            {
                this.Validator = new BaseDomainValidator();
            }

            var result = this.Validator.Validate(entity);
            var isValid = false;
            if (result.IsValid)
            {
                isValid = true;
            }
            else
            {
                Utils.LogErrors(result);
                return false;
            }

            if (isValid == true)
            {
                this.Repository.Insert(entity);
            }

            this.Validator = new DomainValidator();
            return true;
        }
    }
}
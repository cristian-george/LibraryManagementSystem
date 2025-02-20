﻿// <copyright file="DomainService.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Services
{
    using Library.DataLayer.Interfaces;
    using Library.DomainLayer.Extensions;
    using Library.DomainLayer.Models;
    using Library.DomainLayer.Validators;
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
                Logging.LogErrors(result);
                return false;
            }

            if (!this.CheckAdditionalRules(entity))
            {
                Logging.LogErrors($"Additional rules for {entity} were not met!");
                return false;
            }

            return this.Repository.Insert(entity);
        }

        /// <summary>
        /// Check additional rules.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <returns>Bool.</returns>
        public bool CheckAdditionalRules(Domain domain)
        {
            if (this.Repository.GetByName(domain.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
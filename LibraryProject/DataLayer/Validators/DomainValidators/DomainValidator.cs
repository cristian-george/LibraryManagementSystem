// <copyright file="DomainValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators.DomainValidators
{
    using System.Collections.Generic;
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class DomainValidator.
    /// Implements the <see cref="AbstractValidator{Domain}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Domain}" />
    public class DomainValidator : AbstractValidator<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidator" /> class.
        /// </summary>
        public DomainValidator()
        {
            _ = this.RuleFor(d => d.Name)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid");

            _ = this.RuleFor(b => b.ChildrenDomains)
                .NotNull().WithMessage("Null {PropertyName}");
        }

        /// <summary>
        /// Haves the entities.
        /// </summary>
        /// <typeparam name="T"> ceva. </typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool HaveEntities<T>(ICollection<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
// <copyright file="DomainValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Domain validator.
    /// </summary>
    public class DomainValidator : AbstractValidator<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidator" /> class.
        /// </summary>
        public DomainValidator()
        {
            _ = this.RuleFor(d => d.Name)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length");

            _ = this.RuleFor(d => d.ParentDomain.Name)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .When(d => d.ParentDomain != null);

            // _ = this.RuleFor(d => d.ChildDomains)
            //    .NotNull().WithMessage("{PropertyName} is null");

            // _ = this.RuleForEach(d => d.ChildDomains).SetValidator(new DomainValidator());
        }
    }
}
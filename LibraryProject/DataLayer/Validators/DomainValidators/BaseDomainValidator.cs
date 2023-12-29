// <copyright file="BaseDomainValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators.DomainValidators
{
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class BaseDomainValidator.
    /// Implements the <see cref="AbstractValidator{Domain}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Domain}" />
    public class BaseDomainValidator : AbstractValidator<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDomainValidator"/> class.
        /// </summary>
        public BaseDomainValidator()
        {
            _ = this.RuleFor(d => d.Name)
               .NotNull().WithMessage("Null {PropertyName}")
               .NotEmpty().WithMessage("{PropertyName} is Empty")
               .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid");

            _ = this.RuleFor(b => b.ChildrenDomains)
                    .NotNull().WithMessage("Null {PropertyName}");
        }
    }
}
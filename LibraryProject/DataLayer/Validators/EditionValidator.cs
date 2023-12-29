// <copyright file="EditionValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class EditionValidator.
    /// Implements the <see cref="AbstractValidator{Edition}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Edition}" />
    public class EditionValidator : AbstractValidator<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionValidator" /> class.
        /// </summary>
        public EditionValidator()
        {
            _ = this.RuleFor(e => e.Publisher)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid");

            _ = this.RuleFor(e => e.Year)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(0, 4).WithMessage("Lenght of {PropertyName} Invalid");

            _ = this.RuleFor(e => e.EditionNumber)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThan(1).WithMessage("{PropertyName} should be greater than 1");

            _ = this.RuleFor(e => e.NumberOfPages)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThan(1).WithMessage("{PropertyName} should be greater than 1");
        }
    }
}
// <copyright file="EditionValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition validator.
    /// </summary>
    public class EditionValidator : Validator<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionValidator" /> class.
        /// </summary>
        public EditionValidator()
        {
            _ = this.RuleFor(e => e.Publisher)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(e => e.Year)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .InclusiveBetween(1850, 2024).WithMessage("{PropertyName} is invalid");

            _ = this.RuleFor(e => e.EditionNumber)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(1).WithMessage("{PropertyName} should be greater than 1");

            _ = this.RuleFor(e => e.NumberOfPages)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(10).WithMessage("{PropertyName} should be greater than 10");

            _ = this.RuleFor(e => e.Stocks)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleFor(e => e.Book).SetValidator(new BookValidator());

            _ = this.RuleForEach(e => e.Stocks).SetValidator(new StockValidator());
        }
    }
}

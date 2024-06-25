// <copyright file="StockValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock validator.
    /// </summary>
    public class StockValidator : Validator<Stock>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockValidator" /> class.
        /// </summary>
        public StockValidator()
        {
            _ = this.RuleFor(s => s.NumberOfBooksForBorrowing)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(5).WithMessage("{PropertyName} should be greater than 5");

            _ = this.RuleFor(s => s.NumberOfBooksForLectureOnly)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(5).WithMessage("{PropertyName} should be greater than 5");

            _ = this.RuleFor(s => s.InitialStock)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(10).WithMessage("{PropertyName} should be greater than 5");

            _ = this.RuleFor(s => s.Edition).SetValidator(new EditionValidator());

            _ = this.RuleFor(s => s.Borrows)
                .NotNull().WithMessage("{PropertyName} is null");

            _ = this.RuleForEach(s => s.Borrows).SetValidator(new BorrowValidator());
        }
    }
}

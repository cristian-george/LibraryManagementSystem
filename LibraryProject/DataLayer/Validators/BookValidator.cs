// <copyright file="BookValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book validator.
    /// </summary>
    public class BookValidator : Validator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator" /> class.
        /// </summary>
        public BookValidator()
        {
            _ = this.RuleFor(b => b.Title)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 100).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Genre)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 45).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Authors)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleFor(b => b.Editions)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleFor(b => b.Domains)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(b => b.Authors).SetValidator(new AuthorValidator());
            _ = this.RuleForEach(b => b.Editions).SetValidator(new EditionValidator());
            _ = this.RuleForEach(b => b.Domains).SetValidator(new DomainValidator());
        }
    }
}

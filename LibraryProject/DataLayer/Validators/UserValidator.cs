// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// User validator.
    /// </summary>
    public class UserValidator : Validator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator" /> class.
        /// </summary>
        public UserValidator()
        {
            _ = this.RuleFor(b => b.FirstName)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.LastName)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Address)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 80).WithMessage("{PropertyName} has invalid length");

            _ = this.RuleFor(a => a.Email)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .EmailAddress().WithMessage("{PropertyName} is invalid")
                .Length(2, 45).WithMessage("{PropertyName} has invalid length");

            _ = this.RuleFor(a => a.PhoneNumber)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(10).WithMessage("{PropertyName} has invalid length")
                .Must(DoesNotContainLetters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(a => a.LibrarianBorrows)
                .NotNull().WithMessage("{PropertyName} is null");

            _ = this.RuleFor(a => a.ReaderBorrows)
                .NotNull().WithMessage("{PropertyName} is null");

            _ = this.RuleForEach(a => a.LibrarianBorrows).SetValidator(new BorrowValidator());
            _ = this.RuleForEach(a => a.ReaderBorrows).SetValidator(new BorrowValidator());
        }
    }
}
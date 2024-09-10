// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// User validator.
    /// </summary>
    public class UserValidator : AbstractValidator<User>
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

            // _ = this.RuleForEach(a => a.LibrarianBorrows).SetValidator(new BorrowValidator());
            // _ = this.RuleForEach(a => a.ReaderBorrows).SetValidator(new BorrowValidator());
        }

        /// <summary>
        /// Check if a string has valid characters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>bool.</returns>
        public static bool HasValidCharacters(string name)
        {
            if (name == null)
            {
                return false;
            }

            name = name.Replace(" ", string.Empty);
            name = name.Replace("-", string.Empty);
            return name.All(char.IsLetter);
        }

        /// <summary>
        /// Check if a string does not contain letters.
        /// </summary>
        /// <param name="phoneNumber"> The phone number. </param>
        /// <returns> bool. </returns>
        protected static bool DoesNotContainLetters(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return false;
            }

            return phoneNumber.Any(x => !char.IsLetter(x));
        }
    }
}
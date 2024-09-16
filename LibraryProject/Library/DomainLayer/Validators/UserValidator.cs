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
            ApplyNameRules(this.RuleFor(u => u.FirstName), 2, 50);
            ApplyNameRules(this.RuleFor(u => u.LastName), 2, 50);

            _ = this.RuleFor(u => u.Address)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 80).WithMessage("{PropertyName} has invalid length");

            _ = this.RuleFor(u => u.Email)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .EmailAddress().WithMessage("{PropertyName} is invalid")
                .Length(2, 45).WithMessage("{PropertyName} has invalid length");

            _ = this.RuleFor(u => u.PhoneNumber)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(10).WithMessage("{PropertyName} has invalid length")
                .Must(DoesNotContainLetters).WithMessage("{PropertyName} contains invalid characters");
        }

        /// <summary>
        /// Applies common validation rules to a string property, such as checking for null,
        /// non-empty values, enforcing length constraints, and ensuring valid characters.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder used to define validation rules for a specific property.</param>
        /// <param name="lower">The minimum number of characters allowed for the string property.</param>
        /// <param name="upper">The maximum number of characters allowed for the string property.</param>
        public static void ApplyNameRules<T>(IRuleBuilder<T, string> ruleBuilder, int lower, int upper)
        {
            _ = ruleBuilder
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(lower, upper).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");
        }

        /// <summary>
        /// Check if a string has valid characters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Bool.</returns>
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
        /// <returns>Bool.</returns>
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
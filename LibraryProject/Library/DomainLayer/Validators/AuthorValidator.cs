// <copyright file="AuthorValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Author validator.
    /// </summary>
    public class AuthorValidator : AbstractValidator<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorValidator" /> class.
        /// </summary>
        public AuthorValidator()
        {
            ApplyNameRules(this.RuleFor(a => a.FirstName), 2, 50);
            ApplyNameRules(this.RuleFor(a => a.LastName), 2, 50);

            _ = this.RuleFor(a => a.Books)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(a => a.Books).SetValidator(new BookValidator());
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
        /// Check if a collection has entities.
        /// </summary>
        /// <typeparam name="T">Template type.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>Bool.</returns>
        protected static bool HasEntities<T>(ICollection<T> entities)
        {
            return entities != null && entities.Count != 0;
        }
    }
}
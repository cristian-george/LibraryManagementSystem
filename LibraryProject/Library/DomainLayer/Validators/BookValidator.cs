// <copyright file="BookValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Book validator.
    /// </summary>
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator" /> class.
        /// </summary>
        public BookValidator()
        {
            ApplyNameRules(this.RuleFor(b => b.Title), 2, 100);
            ApplyNameRules(this.RuleFor(b => b.Genre), 2, 45);

            _ = this.RuleFor(b => b.Domains)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(b => b.Domains).SetValidator(new DomainValidator());
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
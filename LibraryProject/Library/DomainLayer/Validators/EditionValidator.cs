// <copyright file="EditionValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Edition validator.
    /// </summary>
    public class EditionValidator : AbstractValidator<Edition>
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

            _ = this.RuleFor(e => e.Book).SetValidator(new BookValidator());
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
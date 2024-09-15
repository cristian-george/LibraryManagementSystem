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
            _ = this.RuleFor(a => a.FirstName)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(a => a.LastName)
                .NotNull().WithMessage("{PropertyName} is null")
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 50).WithMessage("{PropertyName} has invalid length")
                .Must(HasValidCharacters).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(a => a.Books)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(a => a.Books).SetValidator(new BookValidator());
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
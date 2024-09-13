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

            // _ = this.RuleFor(b => b.Editions)
            //    .NotNull().WithMessage("{PropertyName} is null")
            //    .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleFor(b => b.Domains)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(b => b.Authors).SetValidator(new AuthorValidator());
            // _ = this.RuleForEach(b => b.Editions).SetValidator(new EditionValidator());
            _ = this.RuleForEach(b => b.Domains).SetValidator(new DomainValidator());
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
        /// Check if a collection has entities.
        /// </summary>
        /// <typeparam name="T">Template type.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns> bool. </returns>
        protected static bool HasEntities<T>(ICollection<T> entities)
        {
            return entities != null && entities.Count != 0;
        }
    }
}
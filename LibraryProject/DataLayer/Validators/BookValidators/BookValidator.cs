// <copyright file="BookValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators.BookValidators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DataLayer.Validators;
    using Library.DomainLayer;

    /// <summary>
    /// Class BookValidator.
    /// Implements the <see cref="AbstractValidator{Book}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Book}" />.
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator" /> class.
        /// </summary>
        public BookValidator()
        {
            _ = this.RuleFor(b => b.Title)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Type)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.IsBorrowed)
                .NotNull().WithMessage("Null {PropertyName}");
            _ = this.RuleFor(b => b.LecturesOnlyBook)
                .NotNull().WithMessage("Null {PropertyName}");

            _ = this.RuleFor(b => b.Authors)
                .NotNull().WithMessage("Null {PropertyName}")
                .Must(HasEntities).WithMessage("{PropertyName} is Empty");

            _ = this.RuleForEach(b => b.Authors).ChildRules(author =>
            {
                _ = author.RuleFor(b => b.FirstName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

                _ = author.RuleFor(b => b.LastName)
                    .NotNull().WithMessage("Null {PropertyName}")
                    .NotEmpty().WithMessage("{PropertyName} is Empty")
                    .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                    .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
            });

            _ = this.RuleFor(b => b.Editions)
                .NotNull().WithMessage("Null {PropertyName}")
                .Must(HasEntities).WithMessage("{PropertyName} is Empty");
            _ = this.RuleFor(b => b.Domains)
                .NotNull().WithMessage("Null {PropertyName}")
                .Must(HasEntities).WithMessage("{PropertyName} is Empty");
            _ = this.RuleForEach(b => b.Editions).SetValidator(new EditionValidator());
            _ = this.RuleForEach(b => b.Domains).SetValidator(new DomainValidator());
        }

        /// <summary>
        /// Verify if the name is valid.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> bool. </returns>
        protected static bool BeAValidName(string name)
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
        /// Has the entities.
        /// </summary>
        /// <typeparam name="T"> Template type. </typeparam>
        /// <param name="entities"> The entities. </param>
        /// <returns> bool. </returns>
        protected static bool HasEntities<T>(ICollection<T> entities)
        {
            return entities != null && entities.Count != 0;
        }
    }
}
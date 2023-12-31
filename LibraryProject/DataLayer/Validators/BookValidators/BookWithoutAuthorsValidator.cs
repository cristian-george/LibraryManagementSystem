// <copyright file="BookWithoutAuthorsValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators.BookValidators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DataLayer.Validators;
    using Library.DataLayer.Validators.DomainValidators;
    using Library.DomainLayer;

    /// <summary>
    /// Class BookWithoutAuthorsValidator.
    /// Implements the <see cref="AbstractValidator{Book}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Book}" />
    public class BookWithoutAuthorsValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookWithoutAuthorsValidator"/> class.
        /// </summary>
        public BookWithoutAuthorsValidator()
        {
            _ = this.RuleFor(b => b.Title)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Type)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.IsBorrowed)
                .NotNull().WithMessage("Null {PropertyName}");
            _ = this.RuleFor(b => b.LecturesOnlyBook)
                .NotNull().WithMessage("Null {PropertyName}");

            _ = this.RuleFor(b => b.Authors)
                .NotNull().WithMessage("Null {PropertyName}");

            _ = this.RuleForEach(b => b.Authors).ChildRules(author =>
            {
                _ = author.RuleFor(b => b.FirstName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

                _ = author.RuleFor(b => b.LastName)
                    .NotNull().WithMessage("Null {PropertyName}")
                    .NotEmpty().WithMessage("{PropertyName} is Empty")
                    .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                    .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
            });

            _ = this.RuleFor(b => b.Editions)
                .NotNull().WithMessage("Null {PropertyName}")
                .Must(HaveEntities).WithMessage("{PropertyName} is Empty");
            _ = this.RuleFor(b => b.Domains)
                .NotNull().WithMessage("Null {PropertyName}")
                .Must(HaveEntities).WithMessage("{PropertyName} is Empty");
            _ = this.RuleForEach(b => b.Editions).SetValidator(new EditionValidator());
            _ = this.RuleForEach(b => b.Domains).SetValidator(new DomainValidator());
        }

        /// <summary>
        /// Bes the name of a valid.
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
        /// Haves the entities.
        /// </summary>
        /// <typeparam name="T"> Template type. </typeparam>
        /// <param name="entities"> The entities. </param>
        /// <returns> bool. </returns>
        protected static bool HaveEntities<T>(ICollection<T> entities)
        {
            return entities != null && entities.Count != 0;
        }
    }
}
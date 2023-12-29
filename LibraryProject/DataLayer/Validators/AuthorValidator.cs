// <copyright file="AuthorValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class AuthorValidator.
    /// Implements the <see cref="AbstractValidator{Author}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Author}" />
    public class AuthorValidator : AbstractValidator<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorValidator" /> class.
        /// </summary>
        public AuthorValidator()
        {
            this.RuleFor(a => a.FirstName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                .Must(this.BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            this.RuleFor(a => a.LastName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Lenght of {PropertyName} Invalid")
                .Must(this.BeAValidName).WithMessage("{PropertyName} contains invalid characters");
        }

        /// <summary>
        /// Bes the name of a valid.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> bool. </returns>
        protected bool BeAValidName(string name)
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
        /// <typeparam name="T"> Entities as ICollection of T. </typeparam>
        /// <param name="entities"> The entities. </param>
        /// <returns> bool. </returns>
        protected bool HaveEntities<T>(ICollection<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
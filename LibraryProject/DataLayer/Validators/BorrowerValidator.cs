// <copyright file="BorrowerValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators
{
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class BorrowerValidator.
    /// Implements the <see cref="AbstractValidator{Library.DomainLayer.Person.Borrower}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Library.DomainLayer.Person.Borrower}" />
    public class BorrowerValidator : AbstractValidator<Borrower>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerValidator" /> class.
        /// </summary>
        public BorrowerValidator()
        {
            _ = this.RuleFor(b => b.FirstName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.LastName)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            _ = this.RuleFor(b => b.Address)
                .NotNull().WithMessage("Null {PropertyName}")
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 80).WithMessage("Length of {PropertyName} Invalid");

            _ = this.RuleFor(b => b.Account).SetInheritanceValidator(v =>
            {
                _ = v.Add(new AccountValidator());
            });
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
    }
}
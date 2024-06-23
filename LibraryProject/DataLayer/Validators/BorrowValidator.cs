// <copyright file="BorrowValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators
{
    using System;
    using FluentValidation;
    using Library.DataLayer.Validators.BookValidators;
    using Library.DomainLayer;

    /// <summary>
    /// Class BorrowValidator.
    /// Implements the <see cref="AbstractValidator{Borrow}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Borrow}" />
    public class BorrowValidator : AbstractValidator<Borrow>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowValidator" /> class.
        /// </summary>
        public BorrowValidator()
        {
            _ = this.RuleFor(b => b.Reader).SetInheritanceValidator(v =>
            {
                _ = v.Add(new ReaderValidator());
            });

            _ = this.RuleFor(b => b.NoOfTimeExtended)
               .NotNull().WithMessage("Null {PropertyName}")
               .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} error")
               .LessThan(4).WithMessage("{PropertyName} error");
            _ = this.RuleFor(b => b.BorrowDate)
                .NotNull().WithMessage("Complete Date is not a valid date.");
            _ = this.RuleFor(b => b.EndDate)
                .NotNull().WithMessage("Complete Date is not a valid date.");

            _ = this.RuleFor(b => b.BorrowedBooks)
               .NotNull().WithMessage("Null {PropertyName}");

            _ = this.RuleFor(b => b.BorrowDate)
                .LessThan(DateTime.Now).WithMessage("{PropertyName} is not less than")
                .NotNull().WithMessage("Null {PropertyName}");

            _ = this.RuleFor(b => b.Librarian).SetValidator(new LibrarianValidator());

            _ = this.RuleForEach(b => b.BorrowedBooks).SetValidator(new BookValidator());
        }
    }
}
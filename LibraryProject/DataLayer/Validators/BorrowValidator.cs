// <copyright file="BorrowValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using System;
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Borrow validator.
    /// </summary>
    public class BorrowValidator : Validator<Borrow>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowValidator" /> class.
        /// </summary>
        public BorrowValidator()
        {
            _ = this.RuleFor(b => b.Reader).SetInheritanceValidator(v =>
            {
                _ = v.Add(new UserValidator());
            });

            _ = this.RuleFor(b => b.BorrowDate)
                .NotNull().WithMessage("{PropertyName} is not a valid date");

            _ = this.RuleFor(b => b.ReturnDate)
                .NotNull().WithMessage("{PropertyName} is not a valid date");

            _ = this.RuleFor(b => b.BorrowDate)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .LessThan(DateTime.Now).WithMessage("{PropertyName} must be less than the current date");

            _ = this.RuleFor(b => b.ReturnDate)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .GreaterThan(b => b.BorrowDate).WithMessage("{PropertyName} must be after the borrow date");

            _ = this.RuleFor(b => b.Librarian).SetValidator(new UserValidator());

            _ = this.RuleFor(b => b.Reader).SetValidator(new UserValidator());

            _ = this.RuleFor(b => new { b.Reader, b.Librarian })
                .Must(x => ValidUsers(x.Reader, x.Librarian)).WithMessage("Users are not valid");

            _ = this.RuleFor(b => b.Stocks)
                .NotNull().WithMessage("{PropertyName} is null")
                .Must(HasEntities).WithMessage("{PropertyName} is empty");

            _ = this.RuleForEach(b => b.Stocks).SetValidator(new StockValidator());
        }
    }
}
// <copyright file="BorrowValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Borrow validator.
    /// </summary>
    public class BorrowValidator : AbstractValidator<Borrow>
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
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} must be less than the current date");

            _ = this.RuleFor(b => b.ReturnDate)
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

        /// <summary>
        /// Check if a borrow is correct.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="librarian">Librarian.</param>
        /// <returns>bool.</returns>
        protected static bool ValidUsers(User reader, User librarian)
        {
            int readerCode = (int)EUserType.Reader;
            int librarianCode = (int)EUserType.Librarian;
            int librarianReaderCode = (int)EUserType.LibrarianReader;

            bool checkReader = (int)reader.UserType == readerCode || (int)reader.UserType == librarianReaderCode;
            bool checkLibrarian = (int)librarian.UserType >= librarianCode;

            return checkReader && checkLibrarian;
        }
    }
}
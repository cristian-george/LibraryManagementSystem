﻿// <copyright file="StockValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Stock validator.
    /// </summary>
    public class StockValidator : AbstractValidator<Stock>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockValidator" /> class.
        /// </summary>
        public StockValidator()
        {
            _ = this.RuleFor(b => b.SupplyDate)
                .NotNull().WithMessage("{PropertyName} is not a valid date");

            _ = this.RuleFor(b => b.SupplyDate)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .LessThan(DateTime.Now).WithMessage("{PropertyName} must be less than the current date");

            _ = this.RuleFor(s => s.NumberOfBooksForBorrowing)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should be positive");

            _ = this.RuleFor(s => s.NumberOfBooksForLectureOnly)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(0).WithMessage("{PropertyName} should be strictly positive");

            _ = this.RuleFor(s => s.InitialStock)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThan(0).WithMessage("{PropertyName} should be strictly positive");

            _ = this.RuleFor(s => s.Edition)
                .NotNull().WithMessage("{PropertyName} is null")
                .SetValidator(new EditionValidator());
        }
    }
}

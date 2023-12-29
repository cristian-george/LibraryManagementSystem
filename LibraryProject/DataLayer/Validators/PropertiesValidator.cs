﻿// <copyright file="PropertiesValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The Validators namespace.
/// </summary>
namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer;

    /// <summary>
    /// Class PropertiesModelValidator.
    /// Implements the <see cref="AbstractValidator{Properties}" />.
    /// </summary>
    /// <seealso cref="AbstractValidator{Properties}" />
    public class PropertiesValidator : AbstractValidator<Properties>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesValidator" /> class.
        /// </summary>
        public PropertiesValidator()
        {
            _ = this.RuleFor(p => p.DOMENII)
            .NotNull().WithMessage("Null {PropertyName}")
            .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.C)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.PER)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.D)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.NMC)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.L)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.LIM)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.DELTA)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.NCZ)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
            _ = this.RuleFor(p => p.PERSIMP)
                .NotNull().WithMessage("Null {PropertyName}")
                .GreaterThanOrEqualTo(1);
        }
    }
}
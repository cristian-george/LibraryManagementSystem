// <copyright file="PropertiesValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Properties validator.
    /// </summary>
    public class PropertiesValidator : Validator<Properties>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesValidator" /> class.
        /// </summary>
        public PropertiesValidator()
        {
            _ = this.RuleFor(p => p.Domenii)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Nmc)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.L)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Per)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.C)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.D)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Lim)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Delta)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Ncz)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Persimp)
                .NotNull().WithMessage("{PropertyName} is null")
                .GreaterThanOrEqualTo(1);
        }
    }
}

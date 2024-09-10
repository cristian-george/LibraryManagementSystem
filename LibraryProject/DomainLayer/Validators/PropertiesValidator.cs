// <copyright file="PropertiesValidator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Validators
{
    using FluentValidation;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Properties validator.
    /// </summary>
    public class PropertiesValidator : AbstractValidator<Properties>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesValidator" /> class.
        /// </summary>
        public PropertiesValidator()
        {
            _ = this.RuleFor(p => p.Domenii)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Nmc)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.L)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Per)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.C)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.D)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Lim)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Delta)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Ncz)
                .GreaterThanOrEqualTo(1);

            _ = this.RuleFor(p => p.Persimp)
                .GreaterThanOrEqualTo(1);
        }
    }
}
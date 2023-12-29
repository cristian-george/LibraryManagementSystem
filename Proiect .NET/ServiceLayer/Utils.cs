﻿// <copyright file="Utils.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer
{
    using FluentValidation.Results;
    using NLog;

    /// <summary>
    /// Class Utils.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the errors.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LogErrors(ValidationResult results)
        {
            if (results.IsValid == false)
            {
                foreach (var error in results.Errors)
                {
                    Logger.Error($"{error.ErrorMessage}");
                }

                return false;
            }

            return true;
        }
    }
}
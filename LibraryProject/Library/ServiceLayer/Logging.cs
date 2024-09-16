// <copyright file="Logging.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer
{
    using FluentValidation.Results;
    using NLog;

    /// <summary>
    /// Class Logging.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the validation errors.
        /// </summary>
        /// <param name="results">The results.</param>
        public static void LogErrors(ValidationResult results)
        {
            if (!results.IsValid)
            {
                foreach (var error in results.Errors)
                {
                    LogErrors(error.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="error">The error.</param>
        public static void LogErrors(string error)
        {
            Logger.Error($"{error}");
        }
    }
}
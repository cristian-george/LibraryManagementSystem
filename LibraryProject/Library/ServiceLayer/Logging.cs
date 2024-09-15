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
        /// Logs the errors.
        /// </summary>
        /// <param name="results">The results.</param>
        public static void LogErrors(ValidationResult results)
        {
            if (results.IsValid == false)
            {
                foreach (var error in results.Errors)
                {
                    Logger.Error($"{error.ErrorMessage}");
                }
            }
        }
    }
}
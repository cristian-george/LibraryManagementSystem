// <copyright file="TestUtils.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Tests
{
    using System;

    /// <summary>
    /// Class TestUtils.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Checks if a string contains digits.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <returns>bool.</returns>
        public static bool ContainsDigits(string s)
        {
            foreach (var c in s)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a string contains letters.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <returns>bool.</returns>
        public static bool ContainsLetters(string s)
        {
            foreach (var c in s)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a string can be an e-mail address.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <returns>bool.</returns>
        public static bool IsEmailValid(string s)
        {
            try
            {
                _ = new System.Net.Mail.MailAddress(s);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if first date is higher than second date.
        /// </summary>
        /// <param name="dt1">First date time.</param>
        /// <param name="dt2">Second date time.</param>
        /// <returns>bool.</returns>
        public static bool IsFirstDateHigherThanSecondDate(DateTime dt1, DateTime dt2)
        {
            return dt1 > dt2;
        }
    }
}

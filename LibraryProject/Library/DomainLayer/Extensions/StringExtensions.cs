// <copyright file="StringExtensions.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Extensions
{
    using System.Linq;

    /// <summary>
    /// Extensions for String class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string contains digits.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <returns>Bool.</returns>
        public static bool ContainsDigits(this string s)
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
        /// <returns>Bool.</returns>
        public static bool ContainsLetters(this string s)
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
        /// <returns>Bool.</returns>
        public static bool IsEmail(this string s)
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
        /// Counts the digits.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <returns>int.</returns>
        public static int CountDigits(this string s)
        {
            return s.Select(char.IsDigit).Count();
        }
    }
}
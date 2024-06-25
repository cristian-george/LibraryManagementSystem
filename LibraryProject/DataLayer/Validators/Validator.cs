// <copyright file="Validator.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer.Validators
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Class Validator.
    /// </summary>
    /// <typeparam name="T">Model class.</typeparam>
    public class Validator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Check if a string has valid characters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>bool.</returns>
        protected static bool HasValidCharacters(string name)
        {
            if (name == null)
            {
                return false;
            }

            name = name.Replace(" ", string.Empty);
            name = name.Replace("-", string.Empty);
            return name.All(char.IsLetter);
        }

        /// <summary>
        /// Check if a string does not contain letters.
        /// </summary>
        /// <param name="phoneNumber"> The phone number. </param>
        /// <returns> bool. </returns>
        protected static bool DoesNotContainLetters(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return false;
            }

            return phoneNumber.Any(x => !char.IsLetter(x));
        }

        /// <summary>
        /// Check if a collection has entities.
        /// </summary>
        /// <typeparam name="Type">Template type.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns> bool. </returns>
        protected static bool HasEntities<Type>(ICollection<Type> entities)
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

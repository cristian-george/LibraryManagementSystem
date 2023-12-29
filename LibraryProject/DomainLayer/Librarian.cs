// <copyright file="Librarian.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Librarian.
    /// Implements the <see cref="DomainLayer.Person.Borrower" />.
    /// </summary>
    /// <seealso cref="DomainLayer.Person.Borrower" />
    public class Librarian : Borrower
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is reader.
        /// </summary>
        /// <value><c>null</c> if [is reader] contains no value, <c>true</c> if [is reader]; otherwise, <c>false</c>.</value>
        [Required]
        public bool? IsReader { get; set; }
    }
}
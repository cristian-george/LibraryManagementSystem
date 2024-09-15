// <copyright file="EUserType.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Enums
{
    /// <summary>
    /// User type.
    /// </summary>
    public enum EUserType
    {
        /// <summary>Reader type.</summary>
        Reader = 0,

        /// <summary>Librarian type.</summary>
        Librarian = 1,

        /// <summary>Librarian and reader type.</summary>
        LibrarianReader = 2,
    }
}

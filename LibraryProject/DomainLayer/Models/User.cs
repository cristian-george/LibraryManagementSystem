// <copyright file="User.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using Library.DomainLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// User model.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the user's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the user's address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the user's type.
    /// </summary>
    public EUserType UserType { get; set; }

    /// <summary>
    /// Gets or sets the user's librarian borrows.
    /// </summary>
    [InverseProperty("Librarian")]
    public virtual ICollection<Borrow> LibrarianBorrows { get; set; }

    /// <summary>
    /// Gets or sets the user's reader borrows.
    /// </summary>
    [InverseProperty("Reader")]
    public virtual ICollection<Borrow> ReaderBorrows { get; set; }
}

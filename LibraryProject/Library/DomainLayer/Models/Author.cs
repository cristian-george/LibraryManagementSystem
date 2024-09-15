// <copyright file="Author.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using System.Collections.Generic;
using Library.DomainLayer.Interfaces;

/// <summary>
/// Author model.
/// </summary>
public class Author : IEntity
{
    /// <summary>
    /// Gets or sets the author's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the author's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the author's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the author's books.
    /// </summary>
    public virtual ICollection<Book> Books { get; set; }
}
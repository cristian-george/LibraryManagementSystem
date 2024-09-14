// <copyright file="Book.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using System.Collections.Generic;
using Library.DomainLayer.Interfaces;

/// <summary>
/// Book model.
/// </summary>
public class Book : IEntity
{
    /// <summary>
    /// Gets or sets the book's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the book's title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the book's genre.
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    /// Gets or sets the book's authors.
    /// </summary>
    public virtual ICollection<Author> Authors { get; set; }

    /// <summary>
    /// Gets or sets the book's domains.
    /// </summary>
    public virtual ICollection<Domain> Domains { get; set; }

    /// <summary>
    /// Gets or sets the book's editions.
    /// </summary>
    public virtual ICollection<Edition> Editions { get; set; }
}
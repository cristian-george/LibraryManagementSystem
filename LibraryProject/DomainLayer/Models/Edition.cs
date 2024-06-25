// <copyright file="Edition.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using Library.DomainLayer.Enums;
using System.Collections.Generic;

/// <summary>
/// Edition model.
/// </summary>
public class Edition
{
    /// <summary>
    /// Gets or sets the edition's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the edition's book id.
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Gets or sets the edition's publisher.
    /// </summary>
    public string Publisher { get; set; }

    /// <summary>
    /// Gets or sets the edition's year.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the edition's number.
    /// </summary>
    public int EditionNumber { get; set; }

    /// <summary>
    /// Gets or sets the edition's number of pages.
    /// </summary>
    public int NumberOfPages { get; set; }

    /// <summary>
    /// Gets or sets the edition's book type.
    /// </summary>
    public EBookType BookType { get; set; }

    /// <summary>
    /// Gets or sets the edition's book.
    /// </summary>
    public virtual Book Book { get; set; }

    /// <summary>
    /// Gets or sets the edition's stocks.
    /// </summary>
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}

﻿// <copyright file="Borrow.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.DomainLayer.Interfaces;

/// <summary>
/// Borrow model.
/// </summary>
public class Borrow : IEntity
{
    /// <summary>
    /// Gets or sets the borrow's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the borrow's reader id.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public int ReaderId { get; set; }

    /// <summary>
    /// Gets or sets the borrow's librarian id.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public int LibrarianId { get; set; }

    /// <summary>
    /// Gets or sets the borrow's date.
    /// </summary>
    public DateTime BorrowDate { get; set; }

    /// <summary>
    /// Gets or sets the borrow's return date.
    /// </summary>
    public DateTime ReturnDate { get; set; }

    /// <summary>
    /// Gets or sets the borrow's librarian.
    /// </summary>
    public virtual User Librarian { get; set; }

    /// <summary>
    /// Gets or sets the borrow's reader.
    /// </summary>
    public virtual User Reader { get; set; }

    /// <summary>
    /// Gets or sets the borrow's stocks.
    /// </summary>
    public virtual ICollection<Stock> Stocks { get; set; }
}
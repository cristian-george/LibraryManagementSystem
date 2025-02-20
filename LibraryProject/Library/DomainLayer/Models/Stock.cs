﻿// <copyright file="Stock.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.DomainLayer.Interfaces;

/// <summary>
/// Stock model.
/// </summary>
public class Stock : IEntity
{
    /// <summary>
    /// Gets or sets the stock's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the stock's edition id.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public int EditionId { get; set; }

    /// <summary>
    /// Gets or sets the stock's supply date.
    /// </summary>
    public DateTime SupplyDate { get; set; }

    /// <summary>
    /// Gets or sets the stock's number of books for borrowing.
    /// </summary>
    public int NumberOfBooksForBorrowing { get; set; }

    /// <summary>
    /// Gets or sets the stock's number of books for lecture only.
    /// </summary>
    public int NumberOfBooksForLectureOnly { get; set; }

    /// <summary>
    /// Gets or sets the stock's initial stock.
    /// </summary>
    public int InitialStock { get; set; }

    /// <summary>
    /// Gets or sets the stock's edition.
    /// </summary>
    public virtual Edition Edition { get; set; }

    /// <summary>
    /// Gets or sets the stock's borrows.
    /// </summary>
    public virtual ICollection<Borrow> Borrows { get; set; }
}
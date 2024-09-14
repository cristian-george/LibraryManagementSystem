// <copyright file="Domain.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer.Models;

using System.Collections.Generic;
using Library.DomainLayer.Interfaces;

/// <summary>
/// Domain model.
/// </summary>
public class Domain : IEntity
{
    /// <summary>
    /// Gets or sets the domain's id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the domain's parent domain id.
    /// </summary>
    public int? ParentDomainId { get; set; }

    /// <summary>
    /// Gets or sets the domain's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the domain's parent.
    /// </summary>
    public virtual Domain ParentDomain { get; set; }

    /// <summary>
    /// Gets or sets the domain's children.
    /// </summary>
    public virtual ICollection<Domain> ChildDomains { get; set; }

    /// <summary>
    /// Gets or sets the domain's books.
    /// </summary>
    public virtual ICollection<Book> Books { get; set; }

    /// <summary>
    /// Equals method.
    /// </summary>
    /// <param name="obj">obj.</param>
    /// <returns>bool.</returns>
    public override bool Equals(object obj)
    {
        return this.Name.Equals((obj as Domain).Name);
    }

    /// <summary>
    /// GetHashCode method.
    /// </summary>
    /// <returns>int.</returns>
    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}

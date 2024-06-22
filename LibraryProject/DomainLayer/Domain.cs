// <copyright file="Domain.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DomainLayer
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class Domain.
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent domain.
        /// </summary>
        /// <value>The parent domain.</value>
        public virtual Domain ParentDomain { get; set; }

        /// <summary>
        /// Gets or sets the children domains.
        /// </summary>
        /// <value>The children domains.</value>
        public virtual ICollection<Domain> ChildrenDomains { get; set; }

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
}
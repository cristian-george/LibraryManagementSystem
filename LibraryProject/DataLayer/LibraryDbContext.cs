// <copyright file="LibraryDbContext.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer
{
    using System.Diagnostics.CodeAnalysis;
    using Library.DomainLayer.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The library context class used to generate the database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LibraryDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Author table.
        /// </summary>
        /// <value>The authors.</value>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the Book table.
        /// </summary>
        /// <value>The books.</value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the Borrow table.
        /// </summary>
        /// <value>The borrows.</value>
        public virtual DbSet<Borrow> Borrows { get; set; }

        /// <summary>
        /// Gets or sets the Domain table.
        /// </summary>
        /// <value>The domains.</value>
        public virtual DbSet<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the Edition table.
        /// </summary>
        /// <value>The editions.</value>
        public virtual DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets the Properties table.
        /// </summary>
        /// <value>The properties.</value>
        public virtual DbSet<Properties> Properties { get; set; }

        /// <summary>
        /// Gets or sets the Stock table.
        /// </summary>
        /// <value>The stocks.</value>
        public virtual DbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// Gets or sets the User table.
        /// </summary>
        /// <value>The users.</value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Method used to configure the options for the context of the database.
        /// </summary>
        /// <param name="optionsBuilder">The optionsBuilder used to configure properties of the server.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var c = "Server=.\\SQLExpress;Database=LibraryDb;User ID=sa;Password=1234;Encrypt=False;";
            var connString =
               "Data Source=DESKTOP-E5915CT\\SQLEXPRESS; " +
               "Initial Catalog=dbLibrary; " +
               "User ID=sa; " +
               "Password=1234; " +
               "Encrypt=False";

            _ = optionsBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer(connString);
        }
    }
}
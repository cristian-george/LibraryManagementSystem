// <copyright file="LibraryContext.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

/// <summary>
/// The DataMapper namespace.
/// </summary>
namespace Library.DataLayer
{
    using Library.DomainLayer;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The library context class used to generate the database.
    /// </summary>
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Librarians table.
        /// </summary>
        /// <value> The librarians. </value>
        public DbSet<Librarian> Librarians { get; set; }

        /// <summary>
        /// Gets or sets the Accounts table.
        /// </summary>
        /// <value> The accounts. </value>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the Borrowers table.
        /// </summary>
        /// <value> The borrowers. </value>
        public DbSet<Borrower> Borrowers { get; set; }

        /// <summary>
        /// Gets or sets the Authors table.
        /// </summary>
        /// <value> The authors. </value>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the Books table.
        /// </summary>
        /// <value> The books. </value>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the Borrow table.
        /// </summary>
        /// <value> The borrow. </value>
        public DbSet<Borrow> Borrow { get; set; }

        /// <summary>
        /// Gets or sets the Domains table.
        /// </summary>
        /// <value> The domains. </value>
        public DbSet<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the Editions table.
        /// </summary>
        /// <value> The editions. </value>
        public DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets the Properties table.
        /// </summary>
        /// <value> The properties. </value>
        public DbSet<Properties> Properties { get; set; }

        /// <summary>
        /// Method used to configure the options for the context of the database.
        /// </summary>
        /// <param name="optionsBuilder"> The optionsBuilder used to configure properties of the server. </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var connString = ConfigurationManager.ConnectionStrings[1].ToString();
            _ = optionsBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer("Data Source=DESKTOP-E5915CT\\SQLEXPRESS; " +
               "Initial Catalog=dbLibrary; User ID=sa; Password=1234; Encrypt=False"/*connString*/);
        }
    }
}
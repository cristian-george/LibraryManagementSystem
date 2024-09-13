// <copyright file="LibraryDbContext.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.DataLayer;

using System.Collections.Generic;
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
        => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=.\\SQLExpress;Database=LibraryDb;User ID=sa;Password=1234;Encrypt=False;");

    /// <summary>
    /// This method is called when the model for a derived context has been initialized, but
    /// before the model has been locked down and used to initialize the context.  The default
    /// implementation of this method does nothing, but it can be overridden in a derived class
    /// such that the model can be further configured before it is locked down.
    /// </summary>
    /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Genre)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksAuthors",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("FK_Authors_BooksAuthors_AuthorId"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Books_BooksAuthors_BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("BooksAuthors");
                    });

            entity.HasMany(d => d.Domains).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksDomains",
                    r => r.HasOne<Domain>().WithMany()
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK_Domains_BooksDomains_DomainId"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Books_BooksDomains_BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "DomainId");
                        j.ToTable("BooksDomains");
                    });
        });

        modelBuilder.Entity<Borrow>(entity =>
        {
            entity.HasOne(d => d.Librarian).WithMany(p => p.LibrarianBorrows)
                .HasForeignKey(d => d.LibrarianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Borrows_LibrarianId");

            entity.HasOne(d => d.Reader).WithMany(p => p.ReaderBorrows)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Borrows_ReaderId");

            entity.HasMany(d => d.Stocks).WithMany(p => p.Borrows)
                .UsingEntity<Dictionary<string, object>>(
                    "BorrowsStocks",
                    r => r.HasOne<Stock>().WithMany()
                        .HasForeignKey("StockId")
                        .HasConstraintName("FK_Borrows_BorrowsStocks_StockId"),
                    l => l.HasOne<Borrow>().WithMany()
                        .HasForeignKey("BorrowId")
                        .HasConstraintName("FK_Borrows_BorrowsStocks_BorrowId"),
                    j =>
                    {
                        j.HasKey("BorrowId", "StockId");
                        j.ToTable("BorrowsStocks");
                    });
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.ParentDomain).WithMany(p => p.ChildDomains).HasForeignKey(d => d.ParentDomainId);
        });

        modelBuilder.Entity<Edition>(entity =>
        {
            entity.Property(e => e.Publisher)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Book).WithMany(p => p.Editions)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Books_Editions_BookId");
        });

        modelBuilder.Entity<Properties>(entity =>
        {
            entity.Property(e => e.Delta).HasColumnName("DELTA");
            entity.Property(e => e.Domenii).HasColumnName("DOMENII");
            entity.Property(e => e.Lim).HasColumnName("LIM");
            entity.Property(e => e.Ncz).HasColumnName("NCZ");
            entity.Property(e => e.Nmc).HasColumnName("NMC");
            entity.Property(e => e.Per).HasColumnName("PER");
            entity.Property(e => e.Persimp).HasColumnName("PERSIMP");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasOne(d => d.Edition).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.EditionId)
                .HasConstraintName("FK_Editions_Stocks_EditionId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);
        });
    }
}

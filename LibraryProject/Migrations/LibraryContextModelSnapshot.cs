﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Library.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace LibraryProject.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.DomainLayer.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.DomainLayer.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BorrowId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsBorrowed")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<bool?>("LecturesOnlyBook")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.DomainLayer.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BorrowDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("LibrarianId")
                        .HasColumnType("int");

                    b.Property<int?>("NoOfTimeExtended")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("LibrarianId");

                    b.ToTable("Borrow");
                });

            modelBuilder.Entity("Library.DomainLayer.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentDomainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ParentDomainId");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("Library.DomainLayer.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<int?>("EditionNumber")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfPages")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Editions");
                });

            modelBuilder.Entity("Library.DomainLayer.Person.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Library.DomainLayer.Person.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Borrowers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Borrower");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Library.DomainLayer.Properties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("C")
                        .HasColumnType("int");

                    b.Property<int?>("D")
                        .HasColumnType("int");

                    b.Property<int?>("DELTA")
                        .HasColumnType("int");

                    b.Property<int?>("DOMENII")
                        .HasColumnType("int");

                    b.Property<int?>("L")
                        .HasColumnType("int");

                    b.Property<int?>("LIM")
                        .HasColumnType("int");

                    b.Property<int?>("NCZ")
                        .HasColumnType("int");

                    b.Property<int?>("NMC")
                        .HasColumnType("int");

                    b.Property<int?>("PER")
                        .HasColumnType("int");

                    b.Property<int?>("PERSIMP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("Library.DomainLayer.Person.Librarian", b =>
                {
                    b.HasBaseType("Library.DomainLayer.Person.Borrower");

                    b.Property<bool?>("IsReader")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Librarian");
                });

            modelBuilder.Entity("Library.DomainLayer.Author", b =>
                {
                    b.HasOne("Library.DomainLayer.Book", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("Library.DomainLayer.Book", b =>
                {
                    b.HasOne("Library.DomainLayer.Borrow", null)
                        .WithMany("BorrowedBooks")
                        .HasForeignKey("BorrowId");
                });

            modelBuilder.Entity("Library.DomainLayer.Borrow", b =>
                {
                    b.HasOne("Library.DomainLayer.Person.Borrower", "Borrower")
                        .WithMany()
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.DomainLayer.Person.Librarian", "Librarian")
                        .WithMany()
                        .HasForeignKey("LibrarianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");

                    b.Navigation("Librarian");
                });

            modelBuilder.Entity("Library.DomainLayer.Domain", b =>
                {
                    b.HasOne("Library.DomainLayer.Book", null)
                        .WithMany("Domains")
                        .HasForeignKey("BookId");

                    b.HasOne("Library.DomainLayer.Domain", "ParentDomain")
                        .WithMany("ChildrenDomains")
                        .HasForeignKey("ParentDomainId");

                    b.Navigation("ParentDomain");
                });

            modelBuilder.Entity("Library.DomainLayer.Edition", b =>
                {
                    b.HasOne("Library.DomainLayer.Book", null)
                        .WithMany("Editions")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("Library.DomainLayer.Person.Borrower", b =>
                {
                    b.HasOne("Library.DomainLayer.Person.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Library.DomainLayer.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Domains");

                    b.Navigation("Editions");
                });

            modelBuilder.Entity("Library.DomainLayer.Borrow", b =>
                {
                    b.Navigation("BorrowedBooks");
                });

            modelBuilder.Entity("Library.DomainLayer.Domain", b =>
                {
                    b.Navigation("ChildrenDomains");
                });
        }
    }
}

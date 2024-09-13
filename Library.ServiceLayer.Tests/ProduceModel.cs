// <copyright file="ProduceModel.cs" company="Transilvania University of Brasov">
// Cristian-George Fieraru
// </copyright>

namespace Library.ServiceLayer.Tests
{
    using System;
    using System.Collections.Generic;
    using Library.DomainLayer.Enums;
    using Library.DomainLayer.Models;

    /// <summary>
    /// Class TestUtils.
    /// </summary>
    public static class ProduceModel
    {
        /// <summary>
        /// Gets properties model.
        /// </summary>
        /// <returns> A model for properties entity. </returns>
        public static Properties GetPropertiesModel()
        {
            return new Properties()
            {
                Domenii = 2,
                Nmc = 2,
                L = 2,
                C = 2,
                D = 2,
                Lim = 2,
                Delta = 2,
                Ncz = 2,
                Persimp = 2,
                Per = 2,
            };
        }

        /// <summary>
        /// Gets reader model.
        /// </summary>
        /// <returns>Reader.</returns>
        public static User GetReaderModel()
        {
            var reader = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                Email = "cristian.fieraru@gmail.com",
                PhoneNumber = "1234567890",
                UserType = EUserType.Reader,
            };

            return reader;
        }

        /// <summary>
        /// Gets reader model.
        /// </summary>
        /// <returns>Reader.</returns>
        public static User GetLibrarianModel()
        {
            var reader = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                Email = "cristian.fieraru@gmail.com",
                PhoneNumber = "1234567890",
                UserType = EUserType.Librarian,
            };

            return reader;
        }

        /// <summary>
        /// Gets reader model.
        /// </summary>
        /// <returns>Reader.</returns>
        public static User GetLibrarianReaderModel()
        {
            var reader = new User()
            {
                FirstName = "Cristian",
                LastName = "Fieraru",
                Address = "str. Strada, nr. 30",
                Email = "cristian.fieraru@gmail.com",
                PhoneNumber = "1234567890",
                UserType = EUserType.LibrarianReader,
            };

            return reader;
        }

        /// <summary>
        /// Gets domain model.
        /// </summary>
        /// <returns>Domain.</returns>
        public static Domain GetRootDomainModel()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
                ChildDomains = new List<Domain>(),
            };

            return domain;
        }

        /// <summary>
        /// Gets domain with one subdomain.
        /// </summary>
        /// <returns>Domain.</returns>
        public static Domain GetDomainWithSubdomainModel()
        {
            var domain = new Domain()
            {
                Name = "Stiinta",
            };

            var subdomain = new Domain()
            {
                Name = "Matematica",
                ParentDomain = domain,
            };

            return domain;
        }

        /// <summary>
        /// Gets science domain model.
        /// </summary>
        /// <returns>A model for domain entity.</returns>
        public static Domain GetScienceDomainModel()
        {
            return new Domain()
            {
                Name = "Stiinta",
                ParentDomain = null,
                ChildDomains = new List<Domain>()
                {
                    new ()
                    {
                        Name = "Matematica",
                        ChildDomains = new List<Domain>(),
                    },
                    new ()
                    {
                        Name = "Fizica",
                        ChildDomains = new List<Domain>(),
                    },
                    new ()
                    {
                        Name = "Chimie",
                        ChildDomains = new List<Domain>(),
                    },
                    new ()
                    {
                        Name = "Informatica",
                        ChildDomains = new List<Domain>()
                        {
                            new ()
                            {
                                Name = "Algoritmi",
                                ChildDomains = new List<Domain>()
                                {
                                    new ()
                                    {
                                        Name = "Algoritmi fundamentali",
                                        ChildDomains = new List<Domain>(),
                                    },
                                    new ()
                                    {
                                        Name = "Algoritmica grafurilor",
                                        ChildDomains = new List<Domain>(),
                                    },
                                    new ()
                                    {
                                        Name = "Algoritmi cuantici",
                                        ChildDomains = new List<Domain>(),
                                    },
                                },
                            },
                            new ()
                            {
                                Name = "Programare",
                                ChildDomains = new List<Domain>(),
                            },
                            new ()
                            {
                                Name = "Baze de date",
                                ChildDomains = new List<Domain>(),
                            },
                            new ()
                            {
                                Name = "Retele de calculatoare",
                                ChildDomains = new List<Domain>(),
                            },
                        },
                    },
                },
            };
        }

        /// <summary>
        /// Gets author model.
        /// </summary>
        /// <returns>Author.</returns>
        public static Author GetAuthorModel()
        {
            var author = new Author()
            {
                FirstName = "Autor",
                LastName = "Autor",
            };

            return author;
        }

        /// <summary>
        /// Gets book model.
        /// </summary>
        /// <returns>Book.</returns>
        public static Book GetBookModel()
        {
            return new Book()
            {
                Title = "Head first design patters",
                Genre = "Programming",
                Authors = new List<Author>(),
                Domains = new List<Domain>(),
            };
        }

        /// <summary>
        /// Gets list of same book.
        /// </summary>
        /// <param name="count">How many books should be in the list.</param>
        /// <returns>Books.</returns>
        public static List<Book> GetListOFSameBook(int count)
        {
            var list = new List<Book>();

            for (int i = 0; i < count; i++)
            {
                list.Add(GetBookModel());
            }

            return list;
        }

        /// <summary>
        /// Gets edition model.
        /// </summary>
        /// <returns>Edition.</returns>
        public static Edition GetEditionModel()
        {
            var edition = new Edition()
            {
                Book = GetBookModel(),
                Publisher = "Cartea studentilor saraci",
                Year = 1999,
                EditionNumber = 5,
                NumberOfPages = 250,
                BookType = DomainLayer.Enums.EBookType.Paperback,
            };

            return edition;
        }

        /// <summary>
        /// Gets stock model.
        /// </summary>
        /// <returns>Stock.</returns>
        public static Stock GetStockModel()
        {
            var stock = new Stock()
            {
                Edition = GetEditionModel(),
                InitialStock = 20,
                NumberOfBooksForBorrowing = 10,
                NumberOfBooksForLectureOnly = 10,
                SupplyDate = DateTime.Now,
            };

            return stock;
        }

        /// <summary>
        /// Gets the borrow model.
        /// </summary>
        /// <returns> A borrow model. </returns>
        public static Borrow GetBorrowModel()
        {
            return new Borrow()
            {
                BorrowDate = DateTime.Now.AddMonths(-1),
                ReturnDate = DateTime.Now.AddMonths(3),
                Reader = GetReaderModel(),
                Librarian = GetLibrarianModel(),
                Stocks = new List<Stock>() { GetStockModel() },
            };
        }
    }
}
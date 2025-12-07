using BookstoreHandler.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BookstoreHandler.UI
{
    public class BookManagerMenu
    {
        private readonly BookstoreContext _context;
        private readonly AuthorManagerMenu _authorMenu;

        public BookManagerMenu(BookstoreContext context, AuthorManagerMenu authorMenu)
        {
            _context = context;
            _authorMenu = authorMenu;
        }

        // Menus
        public static void ShowBookManagerMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Book Management ===");
            Console.WriteLine("[1]. List All Books");
            Console.WriteLine("[2]. Add New Book");
            Console.WriteLine("[3]. Update Book");
            Console.WriteLine("[4]. Delete Book");
            Console.WriteLine("[5]. Back to Main Menu");
            Console.Write("Select an option: ");
        }

        public static void ShowGenreMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Genre ===");
            Console.WriteLine("[1]. Fantasy");
            Console.WriteLine("[2]. Adventure");
            Console.WriteLine("[3]. Romance");
            Console.WriteLine("[4]. ScFi");
        }

        public async Task ShowAuthors()
        {
            await _authorMenu.GetAuthors();
        }

        // Loop
        public void BookManagerMenuLoop()
        {
            bool running = true;
            while (running)
            {
                ShowBookManagerMenu();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ListAllBooks().Wait();
                            break;
                        case 2:
                            AddNewBook().Wait();
                            break;
                        case 3:
                            UpdateBook().Wait();
                            break;
                        case 4:
                            DeleteBook().Wait();
                            break;
                        case 5:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    return;
                }
            }
        }

        // Metods
        public async Task ListAllBooks()
        {
            var bookService = new BookService(_context);

            var books = await bookService.GetBooks();

            Console.Clear();
            Console.WriteLine("=== All Books ===");
            foreach (var book in books)
            {
                Console.WriteLine($"ISBN: {book.Isbn} | Title: {book.Titel} | Author: {book.Författare?.Förnamn} {book.Författare?.Efternamn} | Genre ID: {book.GenreId} | Price: {book.Pris}");
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }

        public async Task AddNewBook()
        {
            var bookService = new BookService(_context);


            Console.Clear();
            Console.WriteLine("=== Add New Book ===");
            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine().Trim();
            if (isbn.Length != 13)
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            await ShowAuthors();
            Console.Write("Enter Author number: ");
            string authorId = Console.ReadLine();
            if(!int.TryParse(authorId, out int authorIdParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            ShowGenreMenu();
            Console.Write("Enter Genre number: ");
            string genreId = Console.ReadLine();
            if(!int.TryParse(genreId, out int genreIdParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Price: ");
            string price = Console.ReadLine();
            if(!decimal.TryParse(price, out decimal priceParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            await bookService.AddBook(isbn, title, authorIdParsed, genreIdParsed, priceParsed);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }

        public async Task UpdateBook()
        {
            var bookService = new BookService(_context);

            Console.Clear();
            Console.WriteLine("=== Update Book ===");
            Console.Write("Enter ISBN of the book to update: ");
            string isbn = Console.ReadLine().Trim();
            if(isbn.Length != 13)
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter New Title: ");
            string title = Console.ReadLine();

            await ShowAuthors();
            Console.Write("Enter New Author number: ");
            string authorId = Console.ReadLine();
            if(!int.TryParse(authorId, out int authorIdParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            ShowGenreMenu();
            Console.Write("Enter New Genre number: ");
            string genreId = Console.ReadLine();
            if(!int.TryParse(genreId, out int genreIdParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter New Price: ");
            string price = Console.ReadLine();
            if(!decimal.TryParse(price, out decimal priceParsed))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            await bookService.UpdateBook(isbn, title, authorIdParsed, genreIdParsed, priceParsed);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();

        }

        public async Task DeleteBook()
        {
            var bookService = new BookService(_context);
            Console.Clear();
            Console.WriteLine("=== Delete Book ===");
            Console.Write("Enter ISBN of the book to delete: ");
            string isbn = Console.ReadLine().Trim();

            if(isbn.Length != 13 )
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            await bookService.DeleteBook(isbn);

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }
    }
}

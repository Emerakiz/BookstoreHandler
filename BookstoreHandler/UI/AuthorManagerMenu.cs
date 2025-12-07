using BookstoreHandler.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreHandler.UI
{
    public class AuthorManagerMenu
    {
        private readonly BookstoreContext _context;
        public AuthorManagerMenu(BookstoreContext context)
        {
            _context = context;
        }

        // Menus
        public static void ShowAuthorManagerMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Author Management ===");
            Console.WriteLine("[1]. List All Authors");
            Console.WriteLine("[2]. Add New Author");
            Console.WriteLine("[3]. Update Author");
            Console.WriteLine("[4]. Delete Author");
            Console.WriteLine("[5]. Back to Main Menu");
            Console.Write("Select an option: ");
        }

        // Loop
        public async Task AuthorManagerLoop()
        {
            bool running = true;
            while (running)
            {
                ShowAuthorManagerMenu();
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        await GetAuthors();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        AddAuthor().Wait();
                        break;
                    case "3":
                        UpdateAuthor().Wait();
                        break;
                    case "4":
                        DeleteAuthor().Wait();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }


        // Metods

        public async Task GetAuthors()
        {
            var authorService = new AuthorService(_context);

            var authors = await authorService.GetAllAuthors();

            Console.Clear();
            Console.WriteLine("=== List of Authors ===");
            foreach (var a in authors)
            {
                Console.WriteLine($"[{a.Id}]. Name: {a.Förnamn} {a.Efternamn}");
            }

        }

        public async Task AddAuthor()
        {
            var authorService = new AuthorService(_context);

            Console.Clear();
            Console.WriteLine("=== Add New Author ===");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            await authorService.AddAuthor(firstName, lastName);

            Console.WriteLine("Author added");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }

        public async Task UpdateAuthor()
        {
            var authorService = new AuthorService(_context);

            Console.Clear();
            GetAuthors().Wait();

            Console.WriteLine("=== Update Author ===");
            Console.Write("Author number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int authorId))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            Console.Write("New First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("New Last Name: ");
            string lastName = Console.ReadLine();

            await authorService.UpdateAuthor(authorId, firstName, lastName);
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();

        }

        public async Task DeleteAuthor()
        {
            var authorService = new AuthorService(_context);

            Console.Clear();
            GetAuthors().Wait();
            Console.WriteLine("=== Delete Author ===");
            Console.Write("Author number to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int authorId))
            {
                Console.WriteLine("Invalid input");
                Console.ReadKey();
                return;
            }

            await authorService.DeleteAuthor(authorId);
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();

        }
    }
}

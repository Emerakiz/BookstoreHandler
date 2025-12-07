using BookstoreHandler.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreHandler.UI
{
    public static class MainMenu
    {

        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Bookstore Management System ===");
            Console.WriteLine("[1]. Manage Inventories");
            Console.WriteLine("[2]. Manage Books");
            Console.WriteLine("[3]. Manager Authors");
            Console.WriteLine("[4]. Exit");
            Console.Write("Select an option: ");
        }

        public static void MainMenuLoop(BookstoreContext context)
        {
            InventoryMenu inventoryMenu = new InventoryMenu(context);
            AuthorManagerMenu authorManagerMenu = new AuthorManagerMenu(context);
            BookManagerMenu bookManagerMenu = new BookManagerMenu(context, authorManagerMenu);

            bool running = true;
            while (running)
            {
                MainMenu.ShowMainMenu();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            inventoryMenu.InventoryMenuLoop();
                            break;
                        case 2:
                            bookManagerMenu.BookManagerMenuLoop();
                            break;
                        case 3:
                            authorManagerMenu.AuthorManagerLoop();
                            break;
                        case 4:
                            running = false;
                            Console.WriteLine("Exit program");
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
    }
}

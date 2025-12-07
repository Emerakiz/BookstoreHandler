using BookstoreHandler.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreHandler.UI
{
    public class InventoryMenu
    {
        private readonly BookstoreContext _context;

        public InventoryMenu(BookstoreContext context)
        {
            _context = context;
        }

        // Menus
        public static void ChooseStore()
        {
            Console.Clear();
            Console.WriteLine("=== Choose Store ===");
            Console.WriteLine("[1]. Scfi Bokhandeln");
            Console.WriteLine("[2]. Adlibris");
            Console.WriteLine("[3]. Akademi Bokhandeln");
            Console.WriteLine("Select an option: ");
        }
        public static void ShowInventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Management ===");
            Console.WriteLine("[1]. List All Inventory");
            Console.WriteLine("[2]. List Inventory by Store");
            Console.WriteLine("[3]. Add New Book to Inventory");
            Console.WriteLine("[4]. Remove Book from Inventory");
            Console.WriteLine("[5]. Remove Collection of a Book from Inventory");
            Console.WriteLine("[6]. Back to Main Menu");
            Console.Write("Select an option: ");
        }

        // Loop
        public void InventoryMenuLoop()
        {
            bool running = true;
            while (running)
            {
                ShowInventoryMenu();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowAllInventory().Wait();
                            break;
                        case 2:
                            ShowInventoryByStore().Wait();
                            break;
                        case 3:
                            AddBookToInventory().Wait();
                            break;
                        case 4:
                            RemoveBookFromInventory().Wait();
                            break;
                        case 5:
                            RemoveCollectionFromInventory().Wait();
                            break;
                        case 6:
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

        // Metods for menu options
        public async Task ShowInventoryByStore()
        {
            var inventoryService = new InventoryService(_context);

            ChooseStore();
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int storeId) || storeId > _context.Butikers.Count())
            {
                Console.WriteLine("Invalid input, try again");
                return;
            }

            var inventory = await inventoryService.GetInventoryByStore(storeId);

            Console.Clear();
            Console.WriteLine("Books in inventory:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"Title: {item.IsbnNavigation?.Titel} | ISBN: {item.Isbn} " +
                    $"| Store: {item.Butik.ButikNamn} | Quantity: {item.Antal}");
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }

        public async Task ShowAllInventory()
        {
            var inventoryService = new InventoryService(_context);

            var inventory = inventoryService.GetAllInventory();

            Console.Clear();
            Console.WriteLine("Books in iventory:");
            foreach (var item in await inventory)
            {
                Console.WriteLine($"Title: {item.IsbnNavigation?.Titel} | ISBN: {item.Isbn} " +
                    $"| Store: {item.Butik.ButikNamn} | Quantity: {item.Antal}");
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }

        public async Task AddBookToInventory()
        {
            var inventoryService = new InventoryService(_context);

            ChooseStore();
            Console.Write("Enter store ID:");
            string storeInput = Console.ReadLine();
            Console.Write("Enter book ISBN:");
            string isbn = Console.ReadLine();
            Console.Write("Enter quantity to add:");
            string quantityInput = Console.ReadLine();

            if (!int.TryParse(storeInput, out int storeId) || storeId > _context.Butikers.Count() ||
                !int.TryParse(quantityInput, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input, try again");
                return;
            }

            await inventoryService.AddBookToInventory(storeId, isbn, quantity);

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }

        public async Task RemoveBookFromInventory()
        {
            var inventoryService = new InventoryService(_context);

            ChooseStore();
            Console.Write("Enter store ID:");
            string storeInput = Console.ReadLine();
            Console.Write("Enter book ISBN:");
            string isbn = Console.ReadLine();
            Console.Write("Enter quantity to add:");
            string quantityInput = Console.ReadLine();

            if (!int.TryParse(storeInput, out int storeId) || storeId > _context.Butikers.Count() ||
                !int.TryParse(quantityInput, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input, try again");
                return;
            }

            await inventoryService.RemoveBook(storeId, isbn, quantity);

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();
        }
        public async Task RemoveCollectionFromInventory()
        {
            var inventoryService = new InventoryService(_context);

            ChooseStore();
            Console.Write("Enter store ID:");
            string storeInput = Console.ReadLine();
            Console.Write("Enter book ISBN:");
            string isbn = Console.ReadLine();

            if (!int.TryParse(storeInput, out int storeId) || storeId > _context.Butikers.Count())
            {
                Console.WriteLine("Invalid input, try again");
                return;
            }

            await inventoryService.RemoveBookCollection(storeId, isbn);

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press any key to return to menu..");
            Console.ReadKey();

        }
    }
}

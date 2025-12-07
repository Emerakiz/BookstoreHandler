using BookstoreHandler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreHandler.Services
{
    public class InventoryService
    {
        private readonly BookstoreContext _context;

        public InventoryService(BookstoreContext context)
        {
            _context = context;
        }

        // List all inventory

        public async Task<List<LagerSaldo>> GetAllInventory()
        {
            return await _context.LagerSaldos
                .Include(l => l.Butik)
                .Include(l => l.IsbnNavigation)
                .ToListAsync();
        }

        // List inventory by store
        public async Task<List<LagerSaldo>> GetInventoryByStore(int storeId)
        {
            return await _context.LagerSaldos
                .Include(l => l.Butik)
                .Include(l => l.IsbnNavigation)
                .Where(l => l.ButikId == storeId)
                .ToListAsync();
        }

        // Add new book to inventory
        public async Task AddBookToInventory(int storeId, string isbn, int quantity)
        {
            var existingBook = _context.LagerSaldos
                .FirstOrDefault(l => l.ButikId == storeId && l.Isbn == isbn);

            if (existingBook != null)
            {
                existingBook.Antal += quantity;
            }
            else
            {
                var newEntry = new LagerSaldo
                {
                    ButikId = storeId,
                    Isbn = isbn,
                    Antal = quantity
                };
                _context.LagerSaldos.Add(newEntry);
            }

            if(existingBook == null)
            {
                Console.WriteLine("Couln't add book");
                return;
            }
            
            Console.WriteLine("Book added");
            await _context.SaveChangesAsync();
        }

        // Remove book from inventory
        public async Task RemoveBook(int storeId, string isbn, int quantity)
        {
            var existingBook = _context.LagerSaldos
                .FirstOrDefault(l => l.ButikId == storeId && l.Isbn == isbn);

            if (existingBook == null)
            {
                Console.WriteLine("Book does not exist");
                return;
            }
            else if (existingBook.Antal < quantity)
            {
                Console.WriteLine("Inventory does not contain enough books to remove asked quantity.");
                return;
            }
            else
            {
                existingBook.Antal -= quantity;
                if (existingBook.Antal <= 0)
                {
                    _context.LagerSaldos.Remove(existingBook);
                }
                
            }
           await _context.SaveChangesAsync();
        }

        // Remove collection of a book from inventory
        public async Task RemoveBookCollection(int storeId, string isbn)
        {
            var existingBook = _context.LagerSaldos
                .FirstOrDefault(l => l.ButikId == storeId && l.Isbn == isbn);

            if (existingBook == null)
            {
                Console.WriteLine("Book does not exist");
                return;
            }
            else
            {
                _context.LagerSaldos.Remove(existingBook);
            }
            await _context.SaveChangesAsync();
        }
    }
}

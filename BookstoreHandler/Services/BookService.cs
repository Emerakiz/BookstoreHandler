using BookstoreHandler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BookstoreHandler.Services
{
    public class BookService
    {
        private readonly BookstoreContext _context;
        public BookService(BookstoreContext context)
        {
            _context = context;
        }

        // List all books
        public async Task<List<Böcker>> GetBooks()
        {
            return await _context.Böckers
                .Include(b => b.Författare)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        // Create book
        public async Task AddBook(string isbn, string title, int author, int genre, decimal price)
        {
            var newBook = new Böcker
            {
                Isbn = isbn,
                Titel = title,
                FörfattareId = author,
                GenreId = genre,
                Pris = price
            };
            _context.Böckers.Add(newBook);
            await _context.SaveChangesAsync();

            Console.WriteLine("Book added..");
            Console.ReadKey();
        }
        // Update book
        public async Task UpdateBook(string isbn, string title, int author, int genre, decimal price)
        {
            var book = await _context.Böckers.FindAsync(isbn);
            if (book == null)
            {
                Console.WriteLine("Book not found");
                Console.ReadKey();
                return;
            }
            book.Titel = title;
            book.FörfattareId = author;
            book.GenreId = genre;
            book.Pris = price;             
            await _context.SaveChangesAsync();

            Console.WriteLine("Book updated..");
            Console.ReadKey();
        }

        // Delete book
        public async Task DeleteBook(string isbn)
        {
            var book = await _context.Böckers.FindAsync(isbn);
            if (book == null)
            {
                Console.WriteLine("Book not found");
                Console.ReadKey();
                return;
            }

            var inventory = _context.LagerSaldos.Where(ls => ls.Isbn == isbn);
            _context.LagerSaldos.RemoveRange(inventory);

            _context.Böckers.Remove(book);
            await _context.SaveChangesAsync();

            Console.WriteLine("Book deleted..");
            Console.ReadKey();
        }
    }
}

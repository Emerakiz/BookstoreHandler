using BookstoreHandler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreHandler.Services
{
    public class AuthorService
    {
        private readonly BookstoreContext _context;

        public AuthorService(BookstoreContext context)
        {
            _context = context;
        }

        // List authors
        public async Task<List<Författare>> GetAllAuthors()
        {
            return await _context.Författares.ToListAsync();
        }

        // Add new author
        public async Task AddAuthor(string firstName, string lastName)
        {
            var newAuthor = new Författare
            {
                Förnamn = firstName,
                Efternamn = lastName
            };
            _context.Författares.Add(newAuthor);
            await _context.SaveChangesAsync();
        }

        // Update author
        public async Task UpdateAuthor(int authorId, string firstName, string lastName)
        {
            var author = await _context.Författares.FindAsync(authorId);
            if (author == null)
            {
                Console.WriteLine("Author not found");
                Console.ReadKey();
                return;
            }
            author.Förnamn = firstName;
            author.Efternamn = lastName;
            await _context.SaveChangesAsync();

            Console.WriteLine("Author updated..");
            Console.ReadKey();

        }

        // Delete author
        public async Task DeleteAuthor(int authorId)
        {
            var author = await _context.Författares.FindAsync(authorId);
            if (author == null)
            {
                Console.WriteLine("Author not found..");
                Console.ReadKey();
                return;
            }

            var books = await _context.Böckers
                .Where(b => b.FörfattareId == authorId)
                .ToListAsync();

            foreach (var book in books)
            {
                book.FörfattareId = null;
            }

            _context.Författares.Remove(author);
            await _context.SaveChangesAsync();

            Console.WriteLine("Author deleted..");
            Console.ReadKey();

        }
    }
}

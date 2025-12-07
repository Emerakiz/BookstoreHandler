using BookstoreHandler.Models;
using BookstoreHandler.Services;
using BookstoreHandler.UI;

namespace BookstoreHandler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var _context = new BookstoreContext();


            MainMenu.MainMenuLoop(_context);

        }
    }
    
}

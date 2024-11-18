using Library.Interfaces;
using Library.Models;
using Library.Services;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInput = new ConsoleUserInput();
            var userOutput = new ConsoleUserOutput();
            var bookCatalog = new JsonBookRepository();
            var menu = new Menu(userInput, userOutput, bookCatalog);
            menu.Show();
        }
    }
}
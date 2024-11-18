using Lab1.Models;
using Lab1.Services;
using Library.Services;

namespace Lab1
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
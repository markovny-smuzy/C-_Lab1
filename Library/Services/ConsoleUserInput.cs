using Library.Interfaces;

namespace Library.Services
{
    public class ConsoleUserInput : IUserInput
    {
        public string ReadInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
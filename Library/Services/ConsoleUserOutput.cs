using Library.Interfaces;

namespace Library.Services
{
    public class ConsoleUserOutput : IUserOutput
    {
        public void WriteOutput(string message)
        {
            Console.WriteLine(message);
        }
    }
}
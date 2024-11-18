using Lab1.Interfaces;

namespace Library.Services;

public class ConsoleUserOutput : IUserOutput
{
    public void Display(string message)
    {
        Console.WriteLine(message);
    }
}
using Lab1.Interfaces;

namespace Lab1.Services;

public class ConsoleUserInput : IUserInput
{
    public string GetUserInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }
}
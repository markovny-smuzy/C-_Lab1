using Library.Interfaces;
using System;
using System.Collections.Generic;
using Library.Models;

namespace Library.Services
{
    public class Menu
    {
        private readonly IUserInput _userInput;
        private readonly IUserOutput _userOutput;
        private readonly IBookCatalog _bookCatalog;

        private readonly Dictionary<int, Action> _menuActions;

        public Menu(IUserInput userInput, IUserOutput userOutput, IBookCatalog bookCatalog)
        {
            _userInput = userInput;
            _userOutput = userOutput;
            _bookCatalog = bookCatalog;

            // Инициализация словаря действий меню
            _menuActions = new Dictionary<int, Action>
            {
                { 1, AddBook },
                { 2, FindByTitle },
                { 3, FindByAuthor },
                { 4, FindByISBN },
                { 5, FindByKeywords },
                { 6, Exit }
            };
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear(); // Очищаем консоль перед каждым отображением меню
                DisplayMenu();
                var choice = GetUserChoice();
                if (_menuActions.ContainsKey(choice))
                {
                    _menuActions[choice].Invoke();
                }
                else
                {
                    _userOutput.WriteOutput("Неверный выбор. Пожалуйста, попробуйте снова.");
                }
            }
        }

        private void DisplayMenu()
        {
            _userOutput.WriteOutput("Добро пожаловать в каталог книг! Выберите действие, введя номер соответствующего пункта:");
            _userOutput.WriteOutput("1. Добавить книгу в каталог");
            _userOutput.WriteOutput("2. Найти книгу по названию");
            _userOutput.WriteOutput("3. Найти книгу по имени автора");
            _userOutput.WriteOutput("4. Найти книгу по ISBN");
            _userOutput.WriteOutput("5. Найти книги по ключевым словам");
            _userOutput.WriteOutput("6. Выйти");
        }

        private int GetUserChoice()
        {
            _userOutput.WriteOutput("Ваш выбор:");
            var input = _userInput.ReadInput();
            int.TryParse(input, out int choice);
            return choice;
        }

        private void AddBook()
        {
            Console.Clear(); // Очистка перед вводом новой информации
            _userOutput.WriteOutput("Введите название книги:");
            var title = _userInput.ReadInput();

            _userOutput.WriteOutput("Введите имя автора:");
            var author = _userInput.ReadInput();

            _userOutput.WriteOutput("Введите жанры (через запятую):");
            var genres = _userInput.ReadInput().Split(',');

            _userOutput.WriteOutput("Введите год публикации книги:");
            var year = int.Parse(_userInput.ReadInput());

            _userOutput.WriteOutput("Введите аннотацию книги:");
            var annotation = _userInput.ReadInput();

            _userOutput.WriteOutput("Введите ISBN книги:");
            var isbn = _userInput.ReadInput();

            var book = new ConcreteBook(title, author, genres, year, annotation, isbn);
            _bookCatalog.AddBook(book);

            _userOutput.WriteOutput("Книга добавлена в каталог.");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void FindByTitle()
        {
            Console.Clear(); // Очистка перед выводом результатов поиска
            _userOutput.WriteOutput("Введите название книги:");
            var titleFragment = _userInput.ReadInput();

            var books = _bookCatalog.FindByTitle(titleFragment);
            foreach (var book in books)
            {
                _userOutput.WriteOutput($"Название: {book.Title}, Автор: {book.Author}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void FindByAuthor()
        {
            Console.Clear(); // Очистка перед выводом результатов поиска
            _userOutput.WriteOutput("Введите имя автора:");
            var authorName = _userInput.ReadInput();

            var books = _bookCatalog.FindByAuthor(authorName);
            foreach (var book in books)
            {
                _userOutput.WriteOutput($"Название: {book.Title}, Автор: {book.Author}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void FindByISBN()
        {
            Console.Clear(); // Очистка перед выводом результата
            _userOutput.WriteOutput("Введите ISBN книги:");
            var isbn = _userInput.ReadInput();

            var book = _bookCatalog.FindByISBN(isbn);
            if (book != null)
            {
                _userOutput.WriteOutput($"Название: {book.Title}, Автор: {book.Author}");
            }
            else
            {
                _userOutput.WriteOutput("Книга не найдена.");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void FindByKeywords()
        {
            Console.Clear(); // Очистка перед выводом результатов поиска
            _userOutput.WriteOutput("Введите ключевые слова (через запятую):");
            var keywords = _userInput.ReadInput().Split(',');

            var results = _bookCatalog.FindByKeywords(keywords);
            foreach (var (book, keywordsFound) in results)
            {
                _userOutput.WriteOutput($"Название: {book.Title}, Автор: {book.Author}");
                _userOutput.WriteOutput($"Найдено ключевых слов: {string.Join(", ", keywordsFound)}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void Exit()
        {
            _userOutput.WriteOutput("Выход из программы. До свидания!");
            Environment.Exit(0); // Завершить программу
        }
    }
}

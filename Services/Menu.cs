using Lab1.Interfaces;
using Lab1.Models;

namespace Lab1.Services;

public class Menu : IMenu
    {
        private readonly IBookCatalog _bookCatalog;
        private readonly IUserInput _userInput;
        private readonly IUserOutput _userOutput;

        public Menu(IBookCatalog bookCatalog, IUserInput userInput, IUserOutput userOutput)
        {
            _bookCatalog = bookCatalog;
            _userInput = userInput;
            _userOutput = userOutput;
        }

        public async Task DisplayMenuAsync()
        {
            bool exit = false;
            while (!exit)
            {
                _userOutput.Display("Добро пожаловать в каталог книг! Выберите действие:");
                _userOutput.Display("1. Добавить книгу в каталог");
                _userOutput.Display("2. Найти книгу по названию");
                _userOutput.Display("3. Найти книгу по имени автора");
                _userOutput.Display("4. Найти книгу по ISBN");
                _userOutput.Display("5. Найти книги по ключевым словам");
                _userOutput.Display("6. Выйти");

                var choice = _userInput.GetUserInput("Ваш выбор:");

                switch (choice)
                {
                    case "1":
                        await AddBookAsync();
                        break;
                    case "2":
                        await SearchByTitleAsync();
                        break;
                    case "3":
                        await SearchByAuthorAsync();
                        break;
                    case "4":
                        await SearchByISBNAsync();
                        break;
                    case "5":
                        await SearchByKeywordsAsync();
                        break;
                    case "6":
                        exit = true;
                        _userOutput.Display("Выход из программы. До свидания!");
                        break;
                    default:
                        _userOutput.Display("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        private async Task AddBookAsync()
        {
            var title = _userInput.GetUserInput("Введите название книги:");
            var author = _userInput.GetUserInput("Введите имя автора:");
            var genres = _userInput.GetUserInput("Введите жанры (через запятую):").Split(',').Select(g => g.Trim())
                .ToList();
            var year = int.Parse(_userInput.GetUserInput("Введите год публикации:"));
            var annotation = _userInput.GetUserInput("Введите аннотацию:");
            var isbn = _userInput.GetUserInput("Введите ISBN:");

            var book = new BookCatalog(title, author, genres, new DateTime(year, 1, 1), annotation, isbn);
            await _bookCatalog.AddBookAsync(book);

            _userOutput.Display("Книга добавлена в каталог!");
        }

        private async Task SearchByTitleAsync()
        {
            var title = _userInput.GetUserInput("Введите название или его фрагмент:");
            var books = await _bookCatalog.SearchBooksByTitleAsync(title);

            DisplayBooks(books);
        }

        private async Task SearchByAuthorAsync()
        {
            var author = _userInput.GetUserInput("Введите имя автора:");
            var books = await _bookCatalog.SearchBooksByAuthorAsync(author);

            DisplayBooks(books);
        }

        private async Task SearchByISBNAsync()
        {
            var isbn = _userInput.GetUserInput("Введите ISBN:");
            var books = await _bookCatalog.SearchBooksByISBNAsync(isbn);

            DisplayBooks(books);
        }

        private async Task SearchByKeywordsAsync()
        {
            var keywords = _userInput.GetUserInput("Введите ключевые слова (через запятую):");
            var books = await _bookCatalog.SearchBooksByKeywordsAsync(keywords);

            DisplayBooks(books);
        }

        private void DisplayBooks(IEnumerable<IBook> books)
        {
            if (!books.Any())
            {
                _userOutput.Display("Книги не найдены.");
                return;
            }

            foreach (var book in books)
            {
                _userOutput.Display(book.GetBookInfo());
            }
        }
    }
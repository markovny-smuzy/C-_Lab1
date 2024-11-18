using Lab1.Interfaces;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace Lab1.Models;

public class JsonBookRepository : BookCatalog
{
    private readonly string _filePath = "books.json";
    private List<IBook> _books;

    public JsonBookRepository()
    {
        if (File.Exists(_filePath))
        {
            var jsonData = File.ReadAllText(_filePath);
            _books = JsonConvert.DeserializeObject<List<IBook>>(jsonData) ?? new List<IBook>();
        }
        else
        {
            _books = new List<IBook>();
        }
    }

    public async Task AddBookAsync(IBook book)
    {
        _books.Add(book);
        await SaveBooksToFileAsync();
    }

    public async Task<IEnumerable<IBook>> SearchBooksByTitleAsync(string title)
    {
        return await Task.Run(() => _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<IEnumerable<IBook>> SearchBooksByAuthorAsync(string author)
    {
        return await Task.Run(() => _books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<IEnumerable<IBook>> SearchBooksByISBNAsync(string isbn)
    {
        return await Task.Run(() => _books.Where(b => b.ISBN.Contains(isbn)));
    }

    public async Task<IEnumerable<IBook>> SearchBooksByKeywordsAsync(string keywords)
    {
        var keywordList = keywords.Split(',').Select(k => k.Trim().ToLower()).ToList();
        return await Task.Run(() => _books.Where(b =>
            keywordList.Any(k => b.Title.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                                 b.Author.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                                 b.Annotation.Contains(k, StringComparison.OrdinalIgnoreCase))));
    }

    private async Task SaveBooksToFileAsync()
    {
        var jsonData = JsonConvert.SerializeObject(_books, (Newtonsoft.Json.Formatting)Formatting.Indented);
        await File.WriteAllTextAsync(_filePath, jsonData);
    }
}
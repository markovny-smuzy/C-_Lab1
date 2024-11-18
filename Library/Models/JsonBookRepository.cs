using Library.Interfaces;
using Library.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Models
{
    public class JsonBookRepository : IBookCatalog
    {
        private const string FilePath = "books.json";
        private readonly List<ConcreteBook> _books;

        public JsonBookRepository()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                _books = JsonConvert.DeserializeObject<List<ConcreteBook>>(json) ?? new List<ConcreteBook>();
            }
            else
            {
                _books = new List<ConcreteBook>();
            }
        }

        public void AddBook(ConcreteBook book)
        {
            _books.Add(book);
            SaveChanges();
        }

        public IEnumerable<IBook> FindByTitle(string titleFragment)
        {
            return _books.Where(b => b.Title.ToLower().Contains(titleFragment.ToLower()));
        }

        public IEnumerable<IBook> FindByAuthor(string authorName)
        {
            return _books.Where(b => b.Author.ToLower().Contains(authorName.ToLower()));
        }

        public IBook FindByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public IEnumerable<(IBook Book, List<string> KeywordsFound)> FindByKeywords(string[] keywords)
        {
            var results = new List<(IBook, List<string>)>();

            foreach (var book in _books)
            {
                var keywordsFound = new List<string>();

                foreach (var keyword in keywords)
                {
                    if (book.ContainsKeyword(keyword))
                    {
                        keywordsFound.Add(keyword);
                    }
                }

                if (keywordsFound.Count > 0)
                {
                    results.Add((book, keywordsFound));
                }
            }

            return results.OrderByDescending(r => r.Item2.Count);
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_books, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}

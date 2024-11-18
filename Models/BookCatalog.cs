using Lab1.Interfaces;

namespace Lab1.Models
{
    public class BookCatalog : IBookCatalog
    {
        private readonly List<IBook> _books;

        public BookCatalog()
        {
            _books = new List<IBook>();
        }

        public BookCatalog(string title, string author, List<string> genres, DateTime dateTime, string annotation, string isbn)
        {
            
        }

        public void AddBook(IBook book)
        {
            _books.Add(book);
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
    }
}
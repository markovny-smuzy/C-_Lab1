using System.Collections.Generic;
using Library.Models;

namespace Library.Interfaces
{
    public interface IBookCatalog
    {
        void AddBook(ConcreteBook book);
        IEnumerable<IBook> FindByTitle(string titleFragment);
        IEnumerable<IBook> FindByAuthor(string authorName);
        IBook FindByISBN(string isbn);
        IEnumerable<(IBook Book, List<string> KeywordsFound)> FindByKeywords(string[] keywords);
    }
}
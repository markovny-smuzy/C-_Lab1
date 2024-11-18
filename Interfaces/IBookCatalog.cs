namespace Lab1.Interfaces;
using System.Collections.Generic;

public interface IBookCatalog
{
    void AddBook(IBook book);
    IEnumerable<IBook> FindByTitle(string titleFragment);
    IEnumerable<IBook> FindByAuthor(string authorName);
    IBook FindByISBN(string isbn);
    IEnumerable<(IBook Book, List<string> KeywordsFound)> FindByKeywords(string[] keywords);
}
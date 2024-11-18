using Lab1.Interfaces;

namespace Lab1.Models;

public abstract class Book : IBook
{
    private List<string> _genres;
    public string Title { get; set; }
    public string Author { get; set; }

    List<string> IBook.Genres
    {
        get => _genres;
        set => _genres = value;
    }

    public DateTime PublicationDate { get; set; }
    public string[] Genres { get; }
    public int PublicationYear { get; }
    public string Annotation { get; set; }
    public string ISBN { get; set; }

    protected Book(string title, string author, string[] genres, int publicationYear, string annotation, string isbn)
    {
        Title = title;
        Author = author;
        Genres = genres;
        PublicationYear = publicationYear;
        Annotation = annotation;
        ISBN = isbn;
    }

    public bool ContainsKeyword(string keyword)
    {
        keyword = keyword.ToLower();
        return Title.ToLower().Contains(keyword) ||
               Author.ToLower().Contains(keyword) ||
               Annotation.ToLower().Contains(keyword);
    }
}

public class ConcreteBook : Book
{
    public ConcreteBook(string title, string author, string[] genres, int publicationYear, string annotation, string isbn)
        : base(title, author, genres, publicationYear, annotation, isbn)
    {
    }
}
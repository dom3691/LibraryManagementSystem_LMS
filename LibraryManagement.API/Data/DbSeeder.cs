using LibraryManagement.API.Models;

namespace LibraryManagement.API.Data;

/// <summary>
/// Class for seeding the database with initial data.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the database with sample books if the database is empty.
    /// </summary>
    public static void SeedBooks(LibraryDbContext context)
    {
        // Only seed if there are no books in the database
        if (context.Books.Any())
        {
            return;
        }

        var sampleBooks = new List<Book>
        {
            new Book
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                ISBN = "978-0-7432-7356-5",
                PublishedDate = new DateTime(1925, 4, 10)
            },
            new Book
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                ISBN = "978-0-06-112008-4",
                PublishedDate = new DateTime(1960, 7, 11)
            },
            new Book
            {
                Title = "1984",
                Author = "George Orwell",
                ISBN = "978-0-452-28423-4",
                PublishedDate = new DateTime(1949, 6, 8)
            },
            new Book
            {
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                ISBN = "978-0-14-143951-8",
                PublishedDate = new DateTime(1813, 1, 28)
            },
            new Book
            {
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                ISBN = "978-0-316-76948-0",
                PublishedDate = new DateTime(1951, 7, 16)
            },
            new Book
            {
                Title = "Lord of the Flies",
                Author = "William Golding",
                ISBN = "978-0-571-05686-5",
                PublishedDate = new DateTime(1954, 9, 17)
            },
            new Book
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                ISBN = "978-0-544-00017-3",
                PublishedDate = new DateTime(1937, 9, 21)
            },
            new Book
            {
                Title = "Brave New World",
                Author = "Aldous Huxley",
                ISBN = "978-0-06-085052-4",
                PublishedDate = new DateTime(1932, 1, 1)
            }
        };

        context.Books.AddRange(sampleBooks);
        context.SaveChanges();
    }
}

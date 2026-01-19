using LibraryManagement.API.DTOs;

namespace LibraryManagement.API.Services;

/// <summary>
/// Interface for book service operations.
/// </summary>
public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync(string? search = null, int pageNumber = 1, int pageSize = 10);
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
    Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto);
    Task<bool> DeleteBookAsync(int id);
    Task<int> GetTotalCountAsync(string? search = null);
}

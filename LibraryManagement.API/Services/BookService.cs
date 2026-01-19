using AutoMapper;
using LibraryManagement.API.Data;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Services;

/// <summary>
/// Service for managing book operations.
/// </summary>
public class BookService : IBookService
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(LibraryDbContext context, IMapper mapper, ILogger<BookService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Books.AsQueryable();

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(b => 
                b.Title.ToLower().Contains(search) || 
                b.Author.ToLower().Contains(search));
        }

        // Apply pagination
        var books = await query
            .OrderBy(b => b.Title)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book == null ? null : _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
    {
        // Check if ISBN already exists
        var existingBook = await _context.Books
            .FirstOrDefaultAsync(b => b.ISBN == createBookDto.ISBN);
        
        if (existingBook != null)
        {
            throw new InvalidOperationException($"A book with ISBN '{createBookDto.ISBN}' already exists.");
        }

        var book = _mapper.Map<Book>(createBookDto);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Book created with ID: {BookId}, Title: {Title}", book.Id, book.Title);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return null;
        }

        // Check if ISBN is being changed and if the new ISBN already exists
        if (book.ISBN != updateBookDto.ISBN)
        {
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == updateBookDto.ISBN && b.Id != id);
            
            if (existingBook != null)
            {
                throw new InvalidOperationException($"A book with ISBN '{updateBookDto.ISBN}' already exists.");
            }
        }

        _mapper.Map(updateBookDto, book);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Book updated with ID: {BookId}", id);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Book deleted with ID: {BookId}", id);
        return true;
    }

    public async Task<int> GetTotalCountAsync(string? search = null)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(b => 
                b.Title.ToLower().Contains(search) || 
                b.Author.ToLower().Contains(search));
        }

        return await query.CountAsync();
    }
}

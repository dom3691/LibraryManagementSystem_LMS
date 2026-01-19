using LibraryManagement.API.DTOs;
using LibraryManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

/// <summary>
/// Controller for managing book operations.
/// All endpoints require JWT authentication.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // Protect all endpoints in this controller
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve all books with optional search and pagination.
    /// </summary>
    /// <param name="search">Optional search term to filter by title or author</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Number of items per page (default: 10)</param>
    /// <returns>List of books</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] string? search = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            // Validate pagination parameters
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var books = await _bookService.GetAllBooksAsync(search, pageNumber, pageSize);
            var totalCount = await _bookService.GetTotalCountAsync(search);

            // Add pagination metadata to response headers
            Response.Headers.Append("X-Total-Count", totalCount.ToString());
            Response.Headers.Append("X-Page-Number", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", pageSize.ToString());
            Response.Headers.Append("X-Total-Pages", Math.Ceiling((double)totalCount / pageSize).ToString());

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving books");
            return StatusCode(500, new { message = "An error occurred while retrieving books" });
        }
    }

    /// <summary>
    /// Retrieve a book by its ID.
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <returns>Book details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookById(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving book with ID: {BookId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the book" });
        }
    }

    /// <summary>
    /// Add a new book to the library.
    /// </summary>
    /// <param name="createBookDto">Book details</param>
    /// <returns>Created book</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var book = await _bookService.CreateBookAsync(createBookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Book creation failed: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the book");
            return StatusCode(500, new { message = "An error occurred while creating the book" });
        }
    }

    /// <summary>
    /// Update an existing book's details.
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <param name="updateBookDto">Updated book details</param>
    /// <returns>Updated book</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var book = await _bookService.UpdateBookAsync(id, updateBookDto);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return Ok(book);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Book update failed: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating book with ID: {BookId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the book" });
        }
    }

    /// <summary>
    /// Delete a book by its ID.
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting book with ID: {BookId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the book" });
        }
    }
}

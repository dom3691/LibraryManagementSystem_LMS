using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.DTOs;

/// <summary>
/// Data Transfer Object for updating an existing book.
/// </summary>
public class UpdateBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Published date is required")]
    public DateTime PublishedDate { get; set; }
}

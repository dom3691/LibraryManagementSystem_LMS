using AutoMapper;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Models;

namespace LibraryManagement.API.Mappings;

/// <summary>
/// AutoMapper profile for entity to DTO mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book mappings
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
    }
}

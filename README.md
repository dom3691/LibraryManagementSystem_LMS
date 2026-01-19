# Library Management System

A RESTful API built with ASP.NET Core Web API for managing a library system. This application provides endpoints for managing books with JWT-based authentication.

## Features

- **Book Management**: Create, read, update, and delete book records
- **JWT Authentication**: Secure endpoints with JWT token-based authentication
- **User Registration & Login**: Basic user authentication system
- **Search Functionality**: Filter books by title or author
- **Pagination**: Efficient data retrieval with pagination support
- **Swagger Documentation**: Interactive API documentation with OpenAPI/Swagger
- **Entity Framework Core**: Data persistence with SQL Server
- **AutoMapper**: Automatic object-to-object mapping
- **Exception Handling**: Comprehensive error handling with meaningful responses

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- SQL Server (LocalDB, SQL Server Express, or full SQL Server instance)
- Visual Studio 2022, Visual Studio Code, or any code editor with .NET support

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd LibraryManagement
```

### 2. Configure the Database Connection

The default connection string in `appsettings.json` uses LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

If you're using a different SQL Server instance, update the connection string accordingly.

### 3. Run the Application

```bash
dotnet run --project LibraryManagement.API
```

The application will:
- Create the database automatically if it doesn't exist
- Seed the database with sample books
- Start the API server (typically at `https://localhost:5001` or `http://localhost:5000`)

### 4. Access Swagger UI

Once the application is running, navigate to:
- **Swagger UI**: `http://localhost:5000` or `https://localhost:5001`
- The Swagger interface provides interactive API documentation and testing capabilities

## API Endpoints

### Authentication Endpoints (No authentication required)

#### Register a New User
```
POST /api/auth/register
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2024-01-02T12:00:00Z",
  "username": "john_doe"
}
```

#### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "usernameOrEmail": "john_doe",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2024-01-02T12:00:00Z",
  "username": "john_doe"
}
```

### Book Endpoints (Authentication required)

All book endpoints require a valid JWT token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

#### Get All Books
```
GET /api/books?search=title&pageNumber=1&pageSize=10
```

**Query Parameters:**
- `search` (optional): Filter by title or author
- `pageNumber` (optional, default: 1): Page number
- `pageSize` (optional, default: 10, max: 100): Items per page

**Response Headers:**
- `X-Total-Count`: Total number of books
- `X-Page-Number`: Current page number
- `X-Page-Size`: Items per page
- `X-Total-Pages`: Total number of pages

#### Get Book by ID
```
GET /api/books/{id}
```

#### Create a New Book
```
POST /api/books
Content-Type: application/json

{
  "title": "The Art of Programming",
  "author": "John Smith",
  "isbn": "978-0-123456-78-9",
  "publishedDate": "2023-01-15T00:00:00Z"
}
```

#### Update a Book
```
PUT /api/books/{id}
Content-Type: application/json

{
  "title": "Updated Title",
  "author": "Updated Author",
  "isbn": "978-0-123456-78-9",
  "publishedDate": "2023-01-15T00:00:00Z"
}
```

#### Delete a Book
```
DELETE /api/books/{id}
```

## Testing the API

### Using Swagger UI

1. Start the application
2. Navigate to the Swagger UI (root URL)
3. Click on the "Authorize" button at the top
4. Register a new user using `/api/auth/register`
5. Copy the JWT token from the response
6. Click "Authorize" again and paste the token (format: `Bearer <token>`)
7. Test the book endpoints

### Using cURL

#### 1. Register a user:
```bash
curl -X POST "https://localhost:5001/api/auth/register" \
  -H "Content-Type: application/json" \
  -d "{\"username\":\"testuser\",\"email\":\"test@example.com\",\"password\":\"password123\"}"
```

#### 2. Login:
```bash
curl -X POST "https://localhost:5001/api/auth/login" \
  -H "Content-Type: application/json" \
  -d "{\"usernameOrEmail\":\"testuser\",\"password\":\"password123\"}"
```

#### 3. Get all books (replace `<token>` with the token from login):
```bash
curl -X GET "https://localhost:5001/api/books" \
  -H "Authorization: Bearer <token>"
```

#### 4. Create a book:
```bash
curl -X POST "https://localhost:5001/api/books" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"New Book\",\"author\":\"Author Name\",\"isbn\":\"978-0-123456-78-9\",\"publishedDate\":\"2023-01-15T00:00:00Z\"}"
```

### Using Postman

1. Import the API endpoints (or manually add them)
2. Register/Login to get a JWT token
3. Set the Authorization header to `Bearer <token>` for all book endpoints
4. Test the CRUD operations

## Project Structure

```
LibraryManagement/
├── LibraryManagement.API/
│   ├── Controllers/
│   │   ├── AuthController.cs      # Authentication endpoints
│   │   └── BooksController.cs     # Book CRUD endpoints
│   ├── Data/
│   │   ├── LibraryDbContext.cs    # EF Core DbContext
│   │   └── DbSeeder.cs            # Database seeding
│   ├── DTOs/
│   │   ├── AuthResponseDto.cs
│   │   ├── BookDto.cs
│   │   ├── CreateBookDto.cs
│   │   ├── LoginDto.cs
│   │   ├── RegisterDto.cs
│   │   └── UpdateBookDto.cs
│   ├── Mappings/
│   │   └── MappingProfile.cs     # AutoMapper configuration
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   ├── Models/
│   │   ├── Book.cs                # Book entity
│   │   └── User.cs                # User entity
│   ├── Services/
│   │   ├── AuthService.cs         # Authentication logic
│   │   ├── BookService.cs         # Book business logic
│   │   ├── IAuthService.cs
│   │   └── IBookService.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs                 # Application startup
├── LibraryManagement.sln
└── README.md
```

## Technologies Used

- **ASP.NET Core 8.0**: Web API framework
- **Entity Framework Core 8.0**: ORM for database operations
- **SQL Server**: Database
- **JWT Bearer Authentication**: Token-based authentication
- **AutoMapper**: Object-to-object mapping
- **Swagger/OpenAPI**: API documentation
- **BCrypt.Net**: Password hashing
- **Logging**: Built-in .NET logging

## Configuration

### JWT Settings

JWT configuration can be modified in `appsettings.json`:

```json
"Jwt": {
  "Key": "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm",
  "Issuer": "LibraryManagement",
  "Audience": "LibraryManagement"
}
```

**Important**: In production, use a secure, randomly generated key and store it securely (e.g., environment variables, Azure Key Vault, etc.).

### Database Connection

Update the connection string in `appsettings.json` to match your SQL Server instance.

## Database Seeding

The application automatically seeds the database with sample books on startup if the database is empty. Sample books include classics like "The Great Gatsby", "1984", "To Kill a Mockingbird", etc.

## Error Handling

The API returns appropriate HTTP status codes:
- `200 OK`: Successful GET, PUT requests
- `201 Created`: Successful POST requests
- `204 No Content`: Successful DELETE requests
- `400 Bad Request`: Invalid input or validation errors
- `401 Unauthorized`: Missing or invalid authentication token
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server errors

## Best Practices Implemented

- ✅ Clean architecture with separation of concerns
- ✅ Dependency Injection
- ✅ DTOs for data transfer
- ✅ AutoMapper for object mapping
- ✅ Comprehensive exception handling
- ✅ Meaningful HTTP status codes
- ✅ Input validation
- ✅ Logging
- ✅ JWT-based authentication
- ✅ RESTful API design
- ✅ Swagger/OpenAPI documentation

## Bonus Features

- ✅ **Search Functionality**: Filter books by title or author
- ✅ **Pagination**: Efficient data retrieval with page number and page size
- ✅ **Swagger/OpenAPI**: Interactive API documentation

## Troubleshooting

### Database Connection Issues

If you encounter database connection errors:
1. Ensure SQL Server (or LocalDB) is installed and running
2. Verify the connection string in `appsettings.json`
3. Check that the database server is accessible

### Port Conflicts

If the default ports are in use, the application will automatically select different ports. Check the console output for the actual URLs.

### JWT Token Issues

- Ensure the token is included in the Authorization header with the "Bearer " prefix
- Check that the token hasn't expired (default: 24 hours)
- Verify the JWT key in `appsettings.json` matches between token generation and validation

## License

This project is created for recruitment exercise purposes.

## Contact

For questions or issues, please refer to the repository or contact the development team.

# Library Management System

A RESTful API built with ASP.NET Core Web API for managing a library system. This application provides endpoints for managing books with JWT-based authentication.

## Quick Start

```bash
# Clone the repository
git clone https://github.com/dom3691/LibraryManagementSystem_LMS.git
cd LibraryManagementSystem_LMS

# Navigate to the API project
cd LibraryManagement.API

# Run the application
dotnet run
```

The application will start on `http://localhost:5000` or `https://localhost:5001`. Open the URL in your browser to access Swagger UI for interactive testing.

**First Steps:**
1. Register a user via `POST /api/auth/register` in Swagger
2. Copy the JWT token from the response
3. Click "Authorize" in Swagger and paste the token
4. Start testing the book endpoints!

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
git clone https://github.com/dom3691/LibraryManagementSystem_LMS.git
cd LibraryManagementSystem_LMS
```

### 2. Verify Prerequisites

Before running the application, ensure you have:

- **.NET 8.0 SDK** installed. Verify by running:
  ```bash
  dotnet --version
  ```
  You should see version 8.0.x or higher.

- **SQL Server** installed and running. The application uses LocalDB by default, which comes with Visual Studio. To verify LocalDB is available:
  ```bash
  sqllocaldb info
  ```

### 3. Configure the Database Connection

The default connection string in `appsettings.json` uses LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

**If you're using a different SQL Server instance**, update the connection string in `LibraryManagement.API/appsettings.json`:

- **SQL Server Express**: `Server=.\SQLEXPRESS;Database=LibraryManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true`
- **Full SQL Server**: `Server=localhost;Database=LibraryManagementDb;User Id=your_username;Password=your_password;`

### 4. Restore Dependencies

Navigate to the project directory and restore NuGet packages:

```bash
cd LibraryManagement.API
dotnet restore
```

### 5. Run the Application

From the `LibraryManagement.API` directory, run:

```bash
dotnet run
```

**Or from the solution root:**

```bash
dotnet run --project LibraryManagement.API
```

**What happens when you run:**
1. The application builds the project
2. Creates the database automatically if it doesn't exist (using `EnsureCreated()`)
3. Seeds the database with 8 sample books (if the database is empty)
4. Starts the API server

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

**Note:** The actual ports may vary. Check the console output for the exact URLs.

### 6. Access Swagger UI

Once the application is running, open your browser and navigate to:

- **Swagger UI**: `http://localhost:5000` or `https://localhost:5001`
- The Swagger interface provides interactive API documentation and testing capabilities

**Note:** If you see a certificate warning for HTTPS, click "Advanced" and "Proceed" (this is normal for development).

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

## How to Test the Application

### Method 1: Using Swagger UI (Recommended for First-Time Testing)

Swagger UI provides an interactive interface to test all API endpoints without needing external tools.

#### Step-by-Step Instructions:

1. **Start the application** (if not already running):
   ```bash
   dotnet run --project LibraryManagement.API
   ```

2. **Open Swagger UI** in your browser:
   - Navigate to `http://localhost:5000` or `https://localhost:5001`
   - You should see the Swagger interface with all available endpoints

3. **Register a new user**:
   - Expand the `POST /api/auth/register` endpoint
   - Click "Try it out"
   - Enter the following JSON in the request body:
     ```json
     {
       "username": "testuser",
       "email": "test@example.com",
       "password": "password123"
     }
     ```
   - Click "Execute"
   - **Copy the `token` value** from the response (you'll need it for authenticated requests)

4. **Authorize with JWT token**:
   - Click the green "Authorize" button at the top right of the Swagger page
   - In the "Value" field, paste your token (format: `Bearer <your-token>`)
   - Click "Authorize", then "Close"

5. **Test Book Endpoints**:
   
   **Get All Books:**
   - Expand `GET /api/books`
   - Click "Try it out"
   - Optionally add query parameters:
     - `search`: Filter by title or author (e.g., "Gatsby")
     - `pageNumber`: Page number (default: 1)
     - `pageSize`: Items per page (default: 10)
   - Click "Execute"
   - View the response with the list of books

   **Get Book by ID:**
   - Expand `GET /api/books/{id}`
   - Click "Try it out"
   - Enter a book ID (e.g., 1)
   - Click "Execute"

   **Create a New Book:**
   - Expand `POST /api/books`
   - Click "Try it out"
   - Enter the request body:
     ```json
     {
       "title": "The Art of Programming",
       "author": "John Smith",
       "isbn": "978-0-123456-78-9",
       "publishedDate": "2023-01-15T00:00:00Z"
     }
     ```
   - Click "Execute"
   - Note the `id` in the response for future operations

   **Update a Book:**
   - Expand `PUT /api/books/{id}`
   - Click "Try it out"
   - Enter the book ID to update
   - Modify the request body with new values
   - Click "Execute"

   **Delete a Book:**
   - Expand `DELETE /api/books/{id}`
   - Click "Try it out"
   - Enter the book ID to delete
   - Click "Execute"

6. **Test Search Functionality**:
   - Use `GET /api/books` with the `search` parameter
   - Try searching for "Gatsby" or "Orwell" to filter books

7. **Test Pagination**:
   - Use `GET /api/books` with `pageNumber=1` and `pageSize=5`
   - Check the response headers for pagination metadata:
     - `X-Total-Count`: Total number of books
     - `X-Page-Number`: Current page
     - `X-Page-Size`: Items per page
     - `X-Total-Pages`: Total pages

### Method 2: Using cURL (Command Line)

#### Prerequisites:
- Windows: Use PowerShell or Git Bash
- Linux/Mac: Use Terminal
- Note: For Windows PowerShell, you may need to escape quotes differently

#### Step-by-Step Instructions:

1. **Register a new user**:
   ```bash
   curl -X POST "http://localhost:5000/api/auth/register" `
     -H "Content-Type: application/json" `
     -d '{\"username\":\"testuser\",\"email\":\"test@example.com\",\"password\":\"password123\"}'
   ```
   
   **Save the token** from the response for the next steps.

2. **Login** (alternative to registration):
   ```bash
   curl -X POST "http://localhost:5000/api/auth/login" `
     -H "Content-Type: application/json" `
     -d '{\"usernameOrEmail\":\"testuser\",\"password\":\"password123\"}'
   ```

3. **Get all books** (replace `<token>` with your actual token):
   ```bash
   curl -X GET "http://localhost:5000/api/books" `
     -H "Authorization: Bearer <token>"
   ```

4. **Get all books with search**:
   ```bash
   curl -X GET "http://localhost:5000/api/books?search=Gatsby" `
     -H "Authorization: Bearer <token>"
   ```

5. **Get all books with pagination**:
   ```bash
   curl -X GET "http://localhost:5000/api/books?pageNumber=1&pageSize=5" `
     -H "Authorization: Bearer <token>"
   ```

6. **Get a specific book by ID**:
   ```bash
   curl -X GET "http://localhost:5000/api/books/1" `
     -H "Authorization: Bearer <token>"
   ```

7. **Create a new book**:
   ```bash
   curl -X POST "http://localhost:5000/api/books" `
     -H "Authorization: Bearer <token>" `
     -H "Content-Type: application/json" `
     -d '{\"title\":\"New Book\",\"author\":\"Author Name\",\"isbn\":\"978-0-123456-78-9\",\"publishedDate\":\"2023-01-15T00:00:00Z\"}'
   ```

8. **Update a book** (replace `{id}` with actual book ID):
   ```bash
   curl -X PUT "http://localhost:5000/api/books/1" `
     -H "Authorization: Bearer <token>" `
     -H "Content-Type: application/json" `
     -d '{\"title\":\"Updated Title\",\"author\":\"Updated Author\",\"isbn\":\"978-0-123456-78-9\",\"publishedDate\":\"2023-01-15T00:00:00Z\"}'
   ```

9. **Delete a book** (replace `{id}` with actual book ID):
   ```bash
   curl -X DELETE "http://localhost:5000/api/books/1" `
     -H "Authorization: Bearer <token>"
   ```

**Note for PowerShell users:** Use backticks (`) for line continuation in PowerShell. For Linux/Mac, use backslashes (\).

### Method 3: Using Postman

#### Step-by-Step Instructions:

1. **Import or Create Collection**:
   - Open Postman
   - Create a new collection named "Library Management API"

2. **Set up Environment Variables** (Optional but recommended):
   - Create a new environment
   - Add variables:
     - `baseUrl`: `http://localhost:5000`
     - `token`: (will be set after login)

3. **Register a User**:
   - Create a new request: `POST {{baseUrl}}/api/auth/register`
   - Go to "Body" tab, select "raw" and "JSON"
   - Enter:
     ```json
     {
       "username": "testuser",
       "email": "test@example.com",
       "password": "password123"
     }
     ```
   - Click "Send"
   - Copy the `token` from response and save it to your environment variable

4. **Login** (Alternative):
   - Create: `POST {{baseUrl}}/api/auth/login`
   - Body:
     ```json
     {
       "usernameOrEmail": "testuser",
       "password": "password123"
     }
     ```
   - Save the token

5. **Configure Authorization for Book Endpoints**:
   - For each book endpoint, go to "Authorization" tab
   - Type: "Bearer Token"
   - Token: `{{token}}` (or paste the token directly)

6. **Test Book Endpoints**:
   - `GET {{baseUrl}}/api/books` - Get all books
   - `GET {{baseUrl}}/api/books?search=Gatsby` - Search books
   - `GET {{baseUrl}}/api/books?pageNumber=1&pageSize=5` - Paginated results
   - `GET {{baseUrl}}/api/books/1` - Get book by ID
   - `POST {{baseUrl}}/api/books` - Create book (with JSON body)
   - `PUT {{baseUrl}}/api/books/1` - Update book (with JSON body)
   - `DELETE {{baseUrl}}/api/books/1` - Delete book

### Method 4: Using .NET HttpClient (C#)

Create a simple console application or use a C# script:

```csharp
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var baseUrl = "http://localhost:5000";

        // Register
        var registerData = new
        {
            username = "testuser",
            email = "test@example.com",
            password = "password123"
        };
        
        var registerJson = JsonSerializer.Serialize(registerData);
        var registerContent = new StringContent(registerJson, Encoding.UTF8, "application/json");
        var registerResponse = await client.PostAsync($"{baseUrl}/api/auth/register", registerContent);
        var registerResult = await registerResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Register: {registerResult}");

        // Extract token (you'll need to parse the JSON)
        // Then use it in subsequent requests:
        // client.DefaultRequestHeaders.Authorization = 
        //     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
}
```

### Testing Checklist

Use this checklist to verify all functionality:

- [ ] Application starts without errors
- [ ] Database is created automatically
- [ ] Sample books are seeded (8 books)
- [ ] Can register a new user
- [ ] Can login with registered user
- [ ] JWT token is received after login
- [ ] Can get all books (with authentication)
- [ ] Can get a specific book by ID
- [ ] Can create a new book
- [ ] Can update an existing book
- [ ] Can delete a book
- [ ] Search functionality works (by title/author)
- [ ] Pagination works correctly
- [ ] Unauthorized requests return 401
- [ ] Invalid requests return appropriate error codes
- [ ] Swagger UI is accessible and functional

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

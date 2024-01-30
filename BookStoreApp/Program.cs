/* using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// In-memory book list
var books = new List<Book>
{
    new Book { Id = 1, Title = "Book A", Year = 2000 },
    new Book { Id = 2, Title = "Book B", Year = 2010 },
    new Book { Id = 3, Title = "Book C", Year = 2020 }
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get all books
app.MapGet("/books", () => books);

// Get a single book by ID
app.MapGet("/books/{id}", (int id) => 
    books.FirstOrDefault(b => b.Id == id) is Book book ? Results.Ok(book) : Results.NotFound());

// Get books by year
app.MapGet("/books/year/{year}", (int year) => 
    books.Where(b => b.Year == year).ToList());

// Create a new book
app.MapPost("/books", (Book book) => 
{
    books.Add(book);
    return Results.Created($"/books/{book.Id}", book);
});

// Update an existing book
app.MapPut("/books/{id}", (int id, Book inputBook) => 
{
    var index = books.FindIndex(b => b.Id == id);
    if (index == -1) return Results.NotFound();

    var book = books[index];
    book.Title = inputBook.Title;
    book.Year = inputBook.Year;
    return Results.NoContent();
});

// Delete a book
app.MapDelete("/books/{id}", (int id) => 
{
    var index = books.FindIndex(b => b.Id == id);
    if (index == -1) return Results.NotFound();

    books.RemoveAt(index);
    return Results.NoContent();
});

app.Run();

class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
}
 */
//  Imports: Essential namespaces are imported at the beginning. These namespaces provide access to ASP.NET Core features, data annotations, collections, and LINQ functionality.

// Application Builder: The WebApplication.CreateBuilder method initializes a new instance of the WebApplicationBuilder, which helps in setting up the application's services and configuration.

// Service Registration: Services like Swagger are added to the DI container for API documentation and exploration.

// Application Instance: builder.Build() creates an instance of the WebApplication, which represents the application itself.

// In-Memory List: The books list acts as a temporary in-memory data store to hold the book records.

// Middleware Configuration: In the development environment, Swagger UI is set up for API documentation. HTTPS redirection is enforced for security.

// Endpoints: Various endpoints are defined to handle CRUD operations:

// GET /books: Fetches all book records.
// GET /books/{id}: Retrieves a single book by its ID.
// GET /books/year/{year}: Gets all books published in a specified year.
// POST /books: Adds a new book to the list.
// PUT /books/{id}: Updates the details of an existing book.
// DELETE /books/{id}: Removes a book from the list.
// Running the Application: app.Run() starts the application, listening for incoming HTTP requests.

// Book Class: Defines the structure of a book entity with Id, Title, and Year as properties. The Id property is annotated with [Key] to signify its role as a unique identifier, although this is more of a formality in this context since there's no real database.
/*  using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container, including Swagger for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// In-memory book list to simulate database storage.
var books = new List<Book>
{
    new Book { Id = 1, Title = "Book A", Year = 2000 },
    new Book { Id = 2, Title = "Book B", Year = 2010 },
    new Book { Id = 3, Title = "Book C", Year = 2020 }
};

// Configure the HTTP request pipeline to include Swagger in development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint to get all books.
app.MapGet("/books", () => books);

// Endpoint to get a single book by ID. Returns 404 if not found.
app.MapGet("/books/{id}", (int id) => 
    books.FirstOrDefault(b => b.Id == id) is Book book ? Results.Ok(book) : Results.NotFound());

// Endpoint to get books published in a specific year.
app.MapGet("/books/year/{year}", (int year) => 
    books.Where(b => b.Year == year).ToList());

// Endpoint to create a new book. The new book is added to the in-memory list.
app.MapPost("/books", (Book book) => 
{
    books.Add(book);
    return Results.Created($"/books/{book.Id}", book);
});

// Endpoint to update an existing book. Returns 404 if the book is not found.
app.MapPut("/books/{id}", (int id, Book inputBook) => 
{
    var index = books.FindIndex(b => b.Id == id);
    if (index == -1) return Results.NotFound();

    var book = books[index];
    book.Title = inputBook.Title;
    book.Year = inputBook.Year;
    return Results.NoContent();
});

// Endpoint to delete a book by ID. Returns 404 if the book is not found.
app.MapDelete("/books/{id}", (int id) => 
{
    var index = books.FindIndex(b => b.Id == id);
    if (index == -1) return Results.NotFound();

    books.RemoveAt(index);
    return Results.NoContent();
});

app.Run();

// Book class with Id, Title, and Year properties.
class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
} */

// To expand the given code, we can introduce more features and improvements to make the API more robust and user-friendly. Here are some enhancements:

// Model Validation: Add data annotations to the Book class for validation and apply validation checks in the POST and PUT endpoints.
// Search Functionality: Add an endpoint to search books by title.
// Detailed Responses: Return more detailed responses for create, update, and delete operations.
// Exception Handling: Add global exception handling to return proper error responses.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container, including Swagger for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// In-memory book list to simulate database storage.
var books = new List<Book>
{
    new Book { Id = 1, Title = "Book A", Year = 2000 },
    new Book { Id = 2, Title = "Book B", Year = 2010 },
    new Book { Id = 3, Title = "Book C", Year = 2020 }
};

// Configure the HTTP request pipeline to include Swagger in development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global exception handling
app.UseExceptionHandler("/error");

// Endpoint for global error handling
app.Map("/error", (HttpContext httpContext) =>
{
    var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem(title: "An unexpected error occurred", detail: exception?.Message);
});

// Endpoint to get all books
app.MapGet("/books", () => books);

// Endpoint to get a single book by ID
app.MapGet("/books/{id}", (int id) => 
    books.FirstOrDefault(b => b.Id == id) is Book book ? Results.Ok(book) : Results.NotFound());

// Endpoint to get books published in a specific year
app.MapGet("/books/year/{year}", (int year) => 
    books.Where(b => b.Year == year).ToList());

// Endpoint to search books by title
app.MapGet("/books/search/{query}", (string query) => 
    books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList());

// Endpoint to create a new book with model validation
app.MapPost("/books", (Book book) =>
{
    if (!Validator.TryValidateObject(book, new ValidationContext(book), null, true))
    {
        return Results.BadRequest("Invalid book data.");
    }
    books.Add(book);
    return Results.Created($"/books/{book.Id}", book);
});

// Endpoint to update an existing book with model validation
app.MapPut("/books/{id}", (int id, Book inputBook) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound("Book not found.");

    if (!Validator.TryValidateObject(inputBook, new ValidationContext(inputBook), null, true))
    {
        return Results.BadRequest("Invalid book data.");
    }

    book.Title = inputBook.Title;
    book.Year = inputBook.Year;
    return Results.Ok(book);
});

// Endpoint to delete a book by ID
app.MapDelete("/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null) return Results.NotFound("Book not found.");

    books.Remove(book);
    return Results.Ok($"Book with ID {id} deleted.");
});

app.Run();

class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Range(1, 2100)]
    public int Year { get; set; }
}

//Explaing app.map
// The app.MapGet method is used to define an endpoint that handles GET requests. The first parameter 
//is the URL path, and the second parameter is a lambda expression that defines the endpoint handler.
// The lambda expression takes the request parameters as input and returns the response. In this case, the response is a list of books.

/*In ASP.NET Core applications, the app.Map... methods are used to configure request handling by mapping incoming requests to specific endpoints based on the request path and, optionally, other attributes like HTTP methods. These methods help organize the handling of different routes and make the application more modular and easier to maintain.

The app.Map... family includes various methods like MapGet, MapPost, MapPut, MapDelete, etc., corresponding to HTTP GET, POST, PUT, DELETE methods, and more. Each of these is used to define how a specific type of request to a given path should be handled.

Example with Comments
Below is a simple example that demonstrates the use of MapGet, MapPost, MapPut, and MapDelete within an ASP.NET Core application. This example will use a simple in-memory list to manage a collection of items, showcasing CRUD operations:

csharp
Copy code
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory list to store items
List<string> items = new List<string> { "Item1", "Item2", "Item3" };

// MapGet to handle GET requests
// Retrieves all items from the list
app.MapGet("/items", () =>
{
    // Simply return the list of items
    return items;
});

// MapGet to handle GET requests for a single item by index
app.MapGet("/items/{id:int}", (int id) =>
{
    // Check if the requested index is within the bounds of the list
    if (id >= 0 && id < items.Count)
    {
        // Return the item at the specified index
        return Results.Ok(items[id]);
    }
    // If the index is out of bounds, return a 404 Not Found response
    return Results.NotFound();
});

// MapPost to handle POST requests
// Adds a new item to the list
app.MapPost("/items", (string newItem) =>
{
    // Add the new item to the list
    items.Add(newItem);
    // Return a 201 Created response, indicating the item was added successfully
    return Results.Created($"/items/{items.Count - 1}", newItem);
});

// MapPut to handle PUT requests
// Updates an existing item at the specified index
app.MapPut("/items/{id:int}", (int id, string updatedItem) =>
{
    // Check if the requested index is within the bounds of the list
    if (id >= 0 && id < items.Count)
    {
        // Update the item at the specified index
        items[id] = updatedItem;
        // Return a 204 No Content response, indicating the update was successful
        return Results.NoContent();
    }
    // If the index is out of bounds, return a 404 Not Found response
    return Results.NotFound();
});

// MapDelete to handle DELETE requests
// Removes an item from the list at the specified index
app.MapDelete("/items/{id:int}", (int id) =>
{
    // Check if the requested index is within the bounds of the list
    if (id >= 0 && id < items.Count)
    {
        // Remove the item at the specified index
        items.RemoveAt(id);
        // Return a 204 No Content response, indicating the deletion was successful
        return Results.NoContent();
    }
    // If the index is out of bounds, return a 404 Not Found response
    return Results.NotFound();
});

app.Run();
Explanation:
MapGet("/items", ...): Defines an endpoint for GET requests to /items. This endpoint returns the entire list of items.

MapGet("/items/{id:int}", ...): Defines an endpoint for GET requests to /items/{id}, where {id} is a path parameter representing the item's index in the list. This endpoint returns a single item by its index.

MapPost("/items", ...): Defines an endpoint for POST requests to /items. This endpoint adds a new item to the list. The new item's content is expected to be in the request body.

MapPut("/items/{id:int}", ...): Defines an endpoint for PUT requests to /items/{id}, allowing the update of an item at a specific index with the data provided in the request body.

MapDelete("/items/{id:int}", ...): Defines an endpoint for DELETE requests to /items/{id}, which removes the item at the specified index from the list.

Each Map... method is used to handle a specific type of HTTP request, allowing you to define a clear and concise API interface for your application. This pattern is especially useful in minimal APIs in .NET 6 and later, where the goal is to reduce boilerplate code and increase productivity.






    */
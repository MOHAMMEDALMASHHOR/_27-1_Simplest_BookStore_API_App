# _27-1_Simplest_BookStore_API_App
A simple .NET 6 Web API for a BookStore application, demonstrating minimal API setup with EF Core and Swagger.
# BookStore API

A simple BookStore Web API built with .NET 6. This API provides a minimal setup to demonstrate fetching books by publication year using Entity Framework Core with an in-memory database.

## Features

- Fetch books by year.
- In-memory database for easy setup and testing.
- Swagger UI for API exploration and testing.

## Running the Application

1. Clone this repository.
2. Navigate to the project directory in your terminal.
3. Run the application using the command `dotnet run`.
4. Access the Swagger UI at `https://localhost:5001/swagger` to interact with the API.

## Endpoints

### GET /books/year/{year}

Fetches all books published in the specified year.

## Technologies

- .NET 6
- Entity Framework Core
- Swagger (Swashbuckle)

## Added Features
- Model Validation: DataAnnotations are used in the Book class for validation. The POST and PUT endpoints check if the provided Book object is valid before proceeding with the operation.

- Search Functionality: A new GET endpoint /books/search/{query} allows users to search for books by title.

- Detailed Responses: The endpoints now return more detailed responses, including error messages for validation failures and confirmation messages for successful deletions.

- Global Exception Handling: Added a middleware for global exception handling to catch unhandled exceptions and return a generic error response. This helps in hiding implementation - - details from the client and provides a consistent error structure.

## License

This project is open source and available under the [MIT License](LICENSE).

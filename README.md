# Product and Order Management System API

This project is a Product and Order Management System API built with **ASP.NET Core**, utilizing **Entity Framework Core**, **LINQ**, **Lambda Expressions**, and **Object-Oriented Programming (OOP)** principles. The system supports managing products, orders, and customers with CRUD operations, validation, and various LINQ queries for data manipulation.

## Project Purpose

The purpose of this project is to create an API that can manage products and orders within an e-commerce platform. The application implements both **DB First** and **Code First** approaches for managing the database and supports key features such as:

- Product management (Create, Read, Update, Delete)
- Order management (Create, Read, Update, Delete)
- Customer management (Code First approach)
- LINQ and Lambda queries for business logic

## Technologies Used

- **ASP.NET Core Web API** (v7.0 and above)
- **Entity Framework Core** for ORM
- **SQL Server** for the database
- **LINQ** and **Lambda Expressions** for querying and processing data
- **Object-Oriented Programming (OOP)** principles (Inheritance, Polymorphism, Interfaces)
- **Swagger** for API documentation
- **Migration** to manage database schema changes

## Features

### Product Operations
- `GET /api/products`: List all products.
- `GET /api/products/{id}`: Get a specific product by ID.
- `POST /api/products`: Add a new product.
- `PUT /api/products/{id}`: Update an existing product.
- `DELETE /api/products/{id}`: Delete a product.
- **Validations**:
  - Name field cannot be empty.
  - Price must be greater than 0.

### Order Operations
- `GET /api/orders`: List all orders.
- `GET /api/orders/{id}`: Get a specific order by ID.
- `POST /api/orders`: Create a new order.
- `GET /api/orders/total`: Calculate and return the total order amount.
- **Validations**:
  - Quantity cannot be null or less than 1.

### Customer Operations (Code First)
- `GET /api/customers`: List all customers.
- `POST /api/customers`: Add a new customer.

### LINQ and Lambda Queries
- Query products with a price greater than 500.
- Find the most ordered product based on order count.
- Calculate the total stock for all products.
- List orders placed after a specific date.

## Project Structure

The project is organized using a **Layered Architecture**:

1. **Controllers**: Contains API endpoints to handle HTTP requests.
2. **Services**: Contains business logic and service methods.
3. **Repositories**: Contains database operations.
4. **Models**: Contains data models (Product, Order, Customer).
5. **Entities**: The BaseEntity class, which includes common properties for all models (e.g., `Id`, `CreatedAt`, `UpdatedAt`).
6. **Interfaces**: For implementing repository and logging operations.

## Requirements

- **.NET 7.0 SDK** or above
- **SQL Server** for the database
- **Visual Studio** for development (or any other IDE that supports .NET)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/FoxXtreme/ProductOrder.API.git
```

### 2. Database Setup

- Update the connection string in `appsettings.json` to your SQL Server instance.
- Run the following command to apply migrations:

```bash
dotnet ef database update
```

### 3. Run the Application

Run the project with the following command:

```bash
dotnet run
```

The application will start and the API will be available at `http://localhost:5000`.

### 4. API Documentation

Swagger is enabled in the project, and you can view the documentation by navigating to:

```
http://localhost:5000/swagger
```

## Notes

- **Error Handling**: The API includes appropriate error handling, including validation checks for inputs and proper status codes.
- **Logging**: The project includes two types of loggers:
  - `FileLogger`: Logs to a file.
  - `DatabaseLogger`: Logs to a database.

Users can switch between these loggers as needed using polymorphism.

## Contribution

Feel free to fork the repository and submit pull requests. Contributions are welcome!
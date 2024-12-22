# ProductsAPI

This project is a RESTful API for managing products using ASP.NET Core. It supports CRUD operations and additional actions like stock increment and decrement. The project follows a code-first approach for database configuration.

## Features
- Retrieve all products
- Retrieve a product by ID
- Create a new product
- Update an existing product
- Delete a product
- Decrement stock of a product
- Add to the stock of a product

## Technologies Used
- ASP.NET Core
- Entity Framework Core (Code-First Approach)
- SQL Server
- Dependency Injection
- Asynchronous Programming

## Prerequisites
1. [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
2. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. [Postman](https://www.postman.com/) or any API testing tool (optional)

## Getting Started

### Step 1: Clone the Repository
```bash
git clone <repository-url>
cd ProductsAPI
```

### Step 2: Set Up the Database
1. Update the `appsettings.json` file with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=ProductsDb;Trusted_Connection=True;"
   }
   ```

2. Run the following commands to create the database and apply migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### Step 3: Run the Application
Run the application using the following command:
```bash
dotnet run
```
The API will be available at `https://localhost:<port>/api/products`.
Swagger is integrated into the project. You can use the Swagger UI to test the endpoints: `https://localhost:<port>/swagger`.

## API Endpoints

### 1. Get All Products
**GET** `/api/products`
- **Response:**
  ```json
  [
      {
          "id": 800000,
          "name": "Product A",
          "description": "Description A",
          "price": 100.0,
          "stock": 10,
          "createdDate": "2024-12-22T00:00:00",
          "updatedDate": "2024-12-22T00:00:00"
      }
  ]
  ```

### 2. Get Product by ID
**GET** `/api/products/{id}`
- **Response:**
  ```json
  {
      "id": 800000,
      "name": "Product A",
      "description": "Description A",
      "price": 100.0,
      "stock": 10,
      "createdDate": "2024-12-22T00:00:00",
      "updatedDate": "2024-12-22T00:00:00"
  }
  ```

### 3. Create Product
**POST** `/api/products`
- **Request Body:**
  ```json
  {
      "name": "New Product",
      "description": "New Description",
      "price": 100.0,
      "stock": 20
  }
  ```
- **Response:**
  ```json
  {
      "id": 2,
      "name": "New Product",
      "description": "New Description",
      "price": 100.0,
      "stock": 20,
      "createdDate": "2024-12-22T00:00:00",
      "updatedDate": "2024-12-22T00:00:00"
  }
  ```

### 4. Update Product
**PUT** `/api/products/{id}`
- **Request Body:**
  ```json
  {
      "name": "Updated Product",
      "description": "Updated Description",
      "price": 120.0,
      "stock": 15
  }
  ```
- **Response:** `204 No Content`

### 5. Delete Product
**DELETE** `/api/products/{id}`
- **Response:** `204 No Content`

### 6. Decrement Stock
**PUT** `/api/products/decrement-stock/{id}/{quantity}`
- **Response:** `200 OK`

### 7. Add to Stock
**PUT** `/api/products/add-to-stock/{id}/{quantity}`
- **Response:** `200 OK`

## Project Structure
- **Controllers**: Contains API controllers
- **Services**: Business logic layer
- **Models**: Contains entity models and DTOs
- **Data**: Handles database context configuration

## Testing
Unit tests are available for the controller and service layers. To run the tests:
```bash
dotnet test
```
You can test the API using the integrated Swagger UI at `https://localhost:<port>/swagger`. This provides an interactive interface to test all the endpoints.



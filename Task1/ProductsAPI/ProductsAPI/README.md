# ProductsAPI

A simple ASP.NET Core Web API for managing a list of products in memory.

## How to Run

1. **Open the solution** in Visual Studio 2022.
2. **Set `ProductsAPI` as the startup project.**
3. **Run the project** using F5 (with debugging) or Ctrl+F5 (without debugging).
4. The API will start and Swagger UI will be available at:  
   `https://localhost:5001/swagger`  
   (or the port shown in your terminal/output window).

## API Endpoints

- `GET /products`  
  Retrieves a list of all products.

- `GET /products/{id}`  
  Retrieves a specific product by its ID.

- `POST /products`  
  Creates a new product.  
  Request body: JSON with `name`, `description`, and `price`.

- `PUT /products/{id}`  
  Updates an existing product.  
  Request body: JSON with `name`, `description`, and `price`.

- `DELETE /products/{id}`  
  Deletes a product by its ID.

## Testing with Swagger

1. Run the API as described above.
2. Open your browser and navigate to:  
   `https://localhost:5001/swagger`
3. Use the interactive UI to explore and test all endpoints.

---

**Validation rules:**  
- `Name` must not be empty.
- `Price` must be positive.

All data is stored in memory and will be lost when the application stops.

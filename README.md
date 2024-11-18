# Product Inventory Management System

> Project for Web Development course.

## Context : 

```
You have been hired as a developer to build a Product Inventory Management System for a retail company. The System should allow users to manage the product catalog, including adding new products, updating existing ones, retrieving product details. Your task is to implement this system using a 3-Tier Architecture approach.
```

## Sprint 1 :

### Add Product

Users should be able to add a new product by providing the following details: 
    - Name: The name of the product (required)
    - Description: A brief description of the product (optional)
    - Price: The price of the product (required; must be greater than zero)
    - Stock Quantity: The quantity of the product available (required; must be non-negative)
    - A new unique Product ID should be automatically generated

Validation:
    - If the product name is empty, return HTTP 400 with an error message.
    - If the price is zero or negative, return HTTP 400 with an error message.
    - If the stock quantity is negative, return HTTP 400 with an error message.
    - On success, return HTTP 200 with a success message and the created product's ID.
    - If a product with the same name already exists (case-insensitive), return HTTP 409 with an error message indicating that the product name must be unique

### Update Product

Users should be able to update an existing product's details: Update the name, description, price, or stock quantity.
The product to be updated will be identified by its Product ID.

Validation: 
    - If the product does not exist, return HTTP 404 with an error message.
    - The price must be positive.
    - The stock quantity must not be negative.
    - If the updated name is empty, return HTTP 400 with an error message.
    - If the updated name already exists for another product (case-insensitive), return HTTP 409 with an error message.
    - On success, return HTTP 200 with an success message and the updated product details.

### Retrieve Product

Users should be able to fetch product details using the Product ID.
    - If the product is found, return HTTP 200 with the product details.
    - If the product is not found, return HTTP 404 with an error message.

## Sprint 2 : Coming Soon...

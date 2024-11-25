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

## Sprint 2 :

You are enhancing the Product Inventory Management System by adding functionnality for creating and managing product bundles. A product bundle groups multiple individual products into a single offering, allowing users to purchase them together at a discount price.

### Create Bundle

Create Product Bundle Users should be able to create a new product bundle with the following details:
    - Bundle Name: The name of the product bundle (required).
    - Description: A brief description of the bundle (optional).
    - Product IDs: A list of IDs for the products included in the bundle (required).
    - Bundle Price: The price of the bundle (required; can be lower than the total price of the individual products).

Validation:
    - If the bundle name is empty, return HTTP 400 with an error message.
    - If the bundle price is zero or negative, return HTTP 400 with an error message.
    - If any product ID in the list does not exist, return HTTP 400 with an error message indicating which product IDs are invalid.
    - If any product in the bundle is out of stock, return HTTP 400 with an error message indicating that the bundle cannot be created.
    - On success, return HTTP 201 with a success message and the created bundle's ID.
    - Price Validation, ensure that the bundle price is:
        - Greater than 60% of the total price of the individual products.
        - Less than 95% of the total price of the individual products.
        - If the bundle price does not meet these conditions, return HTTP 400 with an error message indicating the valid price range

### List Bundles

Provide an endpoint to list all product bundles in the inventory.

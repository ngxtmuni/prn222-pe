# PaperNo_5

## Overview

- Question 1: Console TCP server for product category queries.
- Question 2: Razor Pages `Product` module with `List` and `Create` pages.

## Question 1

Create a console server application.

Requirements:
- Listen on `127.0.0.1:5505`.
- Read one command from client.
- Support these inputs:
  - `ALL`
  - one category name such as `Electronics`, `Clothing`, `Food`
- Return JSON response.
- Return error JSON for invalid command.

Attached resource:
- `1/ClientApp` for testing your server.

## Question 2

Use `2/database.sql` and create Razor Pages.

Create page `/Product/List`:
- Display all products with category.
- Filter by category.
- Sort by name or price.

Create page `/Product/Create`:
- Input product name, price, quantity, category and status.
- Redirect back to list after save.

Validation:
- `ProductName` is required.
- `Price > 0`.
- `Quantity >= 0`.
- Category is required.

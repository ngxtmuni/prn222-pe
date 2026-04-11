# PaperNo_2

## Overview

- Question 1: Console TCP server for broker contract queries.
- Question 2: Razor Pages `Contract` module with `List` and `Create` pages.

## Question 1

Create a console server application.

Requirements:
- Listen on `127.0.0.1:5202`.
- Read one command from client.
- Support these inputs:
  - `ALL`
  - one property type such as `Apartment`, `Commercial`, `Land`, `Villa`
- Return JSON response to client.
- Return an error JSON object for invalid command.

Attached resource:
- `1/ClientApp` is provided for testing your server.

## Question 2

Use `2/database.sql` and create Razor Pages.

Create page `/Contract/List`:
- Display all contracts with broker name.
- Search by contract title.
- Filter by property type.
- Display link to create page.

Create page `/Contract/Create`:
- Input title, property type, signing date, expiration date, broker and value.
- Redirect back to list when saved successfully.

Validation:
- `ContractTitle` is required.
- `PropertyType` is required.
- `ExpirationDate` must be greater than `SigningDate`.
- `Value` must be greater than 0.

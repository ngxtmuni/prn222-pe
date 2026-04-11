# PaperNo_1

## Overview

- Question 1: Console client using TCP to get customer and order data from a provided server app.
- Question 2: Razor Pages `Customer` module with `List` and `Detail` pages.
- Time suggestion: 90 minutes.

## Question 1

Create a console client application.

Requirements:
- Connect to `127.0.0.1:5101`.
- Ask the user to enter one of these values:
  - `ALL`
  - one city name such as `Ha Noi`, `Da Nang`, `Hai Phong`
  - `EXIT`
- Send the entered value to server and read the full response from the network stream.
- Deserialize JSON to a customer list response.
- Display `CustomerId`, `CustomerName`, `Email`, `City`.
- Display total matched record count.
- Handle server connection error.

Attached resource:
- `1/ServerApp/CustomerOrderServer`.

## Question 2

Use `2/database.sql` and create a Razor Pages application.

Create page `/Customer/List`:
- Display all customers.
- Filter by `City` using query string and dropdown.
- Customer name is a clickable link to detail page.

Create page `/Customer/Detail`:
- Accept `id` from query string.
- Display customer information.
- Display all orders of that customer.
- Display `Order Count` and `Total Amount`.

Notes:
- Use the provided `2/list.html` and `2/detail.html` as UI reference.
- Redirect `/` to `/Customer/List`.

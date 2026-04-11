# PaperNo_3

## Overview

- Question 1: Console client for service-room server.
- Question 2: Razor Pages `Services` module with search/list and detail.

## Question 1

Create a console client application.

Requirements:
- Connect to `127.0.0.1:5303`.
- Support these inputs:
  - `ALL`
  - one fee type such as `Daily`, `Weekly`, `Monthly`
  - `EXIT`
- Send command to server and read full response.
- Deserialize JSON list and display data.
- Display total number of matched records.
- Handle connection error.

Attached resource:
- `1/ServerApp/ServiceRoomServer`.

## Question 2

Use `2/database.sql` to build Razor Pages.

Create page `/Services/List`:
- Search by room title.
- Search by fee type.
- Display service list.
- Room title links to detail page.

Create page `/Services/Detail`:
- Accept `id` from query string.
- Display full service information.

Use `2/list.html` and `2/detail.html` as UI reference.

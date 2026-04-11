# PaperNo_4

## Overview

- Question 1: Console application for employee department statistics.
- Question 2: Razor Pages `Employee` module with `List` and `Update` pages.

## Question 1

Create a console application.

Requirements:
- Build a menu with these functions:
  - Show all employees.
  - Filter employees by department.
  - Sort by employee name.
  - Sort by hire date.
  - Count employees by department.
  - Show employee with earliest hire date.
- Data can be initialized in code.
- Display output clearly in console.

## Question 2

Use `2/database.sql` and create Razor Pages.

Create page `/Employee/List`:
- Display all employees with department name.
- Filter by department.
- Sort by employee name or hire date.
- Add update link for each employee.

Create page `/Employee/Update`:
- Accept `id` in query string.
- Update `Position`, `Department`, `HireDate`.
- Redirect back to list when updated successfully.

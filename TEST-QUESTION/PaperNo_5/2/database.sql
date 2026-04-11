CREATE DATABASE PRN222_TestQuestion_Paper5;
GO

USE PRN222_TestQuestion_Paper5;
GO

CREATE TABLE Category
(
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Product
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL,
    Status NVARCHAR(30) NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

INSERT INTO Category (CategoryName)
VALUES
(N'Electronics'),
(N'Clothing'),
(N'Food');
GO

INSERT INTO Product (ProductName, Price, Quantity, Status, CategoryId)
VALUES
(N'Laptop', 1200.00, 10, N'Active', 1),
(N'Headphones', 150.00, 20, N'Active', 1),
(N'T-Shirt', 25.00, 50, N'Active', 2),
(N'Rice 5kg', 15.00, 30, N'Inactive', 3);
GO

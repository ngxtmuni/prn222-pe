CREATE DATABASE PRN222_TestQuestion_Paper7;
GO

USE PRN222_TestQuestion_Paper7;
GO

CREATE TABLE Author
(
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    AuthorName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Book
(
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    PublishYear INT NOT NULL,
    AuthorId INT NOT NULL,
    CONSTRAINT FK_Book_Author FOREIGN KEY (AuthorId) REFERENCES Author(AuthorId)
);
GO

INSERT INTO Author (AuthorName)
VALUES
(N'George Orwell'),
(N'Jane Austen'),
(N'Harper Lee');
GO

INSERT INTO Book (Title, PublishYear, AuthorId)
VALUES
(N'1984', 1949, 1),
(N'Animal Farm', 1945, 1),
(N'Pride and Prejudice', 1813, 2),
(N'To Kill a Mockingbird', 1960, 3);
GO

CREATE DATABASE PRN222_TestQuestion_Paper4;
GO

USE PRN222_TestQuestion_Paper4;
GO

CREATE TABLE Department
(
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Employee
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100) NOT NULL,
    HireDate DATE NOT NULL,
    DepartmentId INT NOT NULL,
    CONSTRAINT FK_Employee_Department FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);
GO

INSERT INTO Department (DepartmentName)
VALUES
(N'Software Development'),
(N'Human Resources'),
(N'Quality Assurance');
GO

INSERT INTO Employee (EmployeeName, Position, HireDate, DepartmentId)
VALUES
(N'John Doe', N'Senior Developer', '2010-03-01', 1),
(N'Jane Smith', N'HR Manager', '2012-07-15', 2),
(N'Emily Johnson', N'QA Engineer', '2015-01-20', 3),
(N'Alice Brown', N'Developer', '2020-04-01', 1);
GO

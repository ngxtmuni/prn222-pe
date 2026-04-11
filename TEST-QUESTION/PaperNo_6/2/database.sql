CREATE DATABASE PRN222_TestQuestion_Paper6;
GO

USE PRN222_TestQuestion_Paper6;
GO

CREATE TABLE Student
(
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Course
(
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Enrollment
(
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    EnrollmentDate DATE NOT NULL,
    PRIMARY KEY (StudentId, CourseId),
    CONSTRAINT FK_Enrollment_Student FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
    CONSTRAINT FK_Enrollment_Course FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
);
GO

INSERT INTO Student (StudentName, Email)
VALUES
(N'Nguyen An', N'an@fpt.edu.vn'),
(N'Tran Binh', N'binh@fpt.edu.vn'),
(N'Le Chi', N'chi@fpt.edu.vn');
GO

INSERT INTO Course (CourseName)
VALUES
(N'PRN222'),
(N'DBI202'),
(N'SWD392');
GO

INSERT INTO Enrollment (StudentId, CourseId, EnrollmentDate)
VALUES
(1, 1, '2026-01-10'),
(1, 2, '2026-01-10'),
(2, 1, '2026-01-11'),
(3, 3, '2026-01-12');
GO

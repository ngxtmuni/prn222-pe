CREATE DATABASE PRN222_TestQuestion_Paper3;
GO

USE PRN222_TestQuestion_Paper3;
GO

CREATE TABLE Services
(
    ServiceId INT IDENTITY(1,1) PRIMARY KEY,
    RoomTitle NVARCHAR(100) NOT NULL,
    FeeType NVARCHAR(30) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(30) NOT NULL,
    Description NVARCHAR(250) NULL
);
GO

INSERT INTO Services (RoomTitle, FeeType, Price, Status, Description)
VALUES
(N'Deluxe Room', N'Daily', 35.00, N'Available', N'Room with balcony'),
(N'Family Room', N'Monthly', 420.00, N'Occupied', N'Room for family stay'),
(N'VIP Room', N'Monthly', 650.00, N'Available', N'Large premium room'),
(N'Standard Room', N'Daily', 25.00, N'Maintenance', N'Basic room');
GO

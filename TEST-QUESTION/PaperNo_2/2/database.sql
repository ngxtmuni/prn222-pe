CREATE DATABASE PRN222_TestQuestion_Paper2;
GO

USE PRN222_TestQuestion_Paper2;
GO

CREATE TABLE Broker
(
    BrokerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL
);
GO

CREATE TABLE Contract
(
    ContractId INT IDENTITY(1,1) PRIMARY KEY,
    ContractTitle NVARCHAR(150) NOT NULL,
    PropertyType NVARCHAR(50) NOT NULL,
    SigningDate DATE NOT NULL,
    ExpirationDate DATE NOT NULL,
    BrokerId INT NOT NULL,
    Value DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Contract_Broker FOREIGN KEY (BrokerId) REFERENCES Broker(BrokerId)
);
GO

INSERT INTO Broker (FullName, Phone)
VALUES
(N'Nguyen Thanh Dat', N'0901234567'),
(N'Tran Thi Hong', N'0908765432'),
(N'Le Minh Khang', N'0912345678');
GO

INSERT INTO Contract (ContractTitle, PropertyType, SigningDate, ExpirationDate, BrokerId, Value)
VALUES
(N'Ban Can Ho Sapphire', N'Apartment', '2024-03-01', '2025-03-01', 1, 4500000000),
(N'Thue Van Phong Quan 1', N'Commercial', '2024-04-10', '2024-10-10', 2, 75000000),
(N'Ban Villa Da Lat', N'Villa', '2024-03-25', '2026-03-25', 2, 12000000000),
(N'Mua Dat Nen View Bien', N'Land', '2024-04-01', '2027-04-01', 3, 5000000000);
GO

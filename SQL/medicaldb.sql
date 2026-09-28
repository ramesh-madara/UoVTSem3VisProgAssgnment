USE [master]
GO

IF DB_ID('medicaldb') IS NOT NULL
BEGIN
    ALTER DATABASE [medicaldb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [medicaldb];
END
GO

CREATE DATABASE [medicaldb]
GO

USE [medicaldb]
GO

CREATE TABLE [Users] (
    [UserId] INT IDENTITY(1,1) PRIMARY KEY,
    [Username] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(50) NOT NULL,
    [Role] VARCHAR(20) NOT NULL
)
GO

CREATE TABLE [Patients] (
    [PatientId] INT IDENTITY(1,1) PRIMARY KEY,
    [FullName] NVARCHAR(100) NOT NULL,
    [NIC] VARCHAR(20) NOT NULL UNIQUE,
    [ContactNumber] VARCHAR(20) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL,
    [BloodGroup] VARCHAR(5) NOT NULL,
    [Gender] VARCHAR(10) NOT NULL,
    [RegisteredDate] DATETIME DEFAULT GETDATE(),
    [Age] INT NOT NULL DEFAULT 30
)
GO

CREATE TABLE [MedicalRecords] (
    [RecordId] INT IDENTITY(1,1) PRIMARY KEY,
    [PatientId] INT FOREIGN KEY REFERENCES [Patients]([PatientId]),
    [Diagnosis] NVARCHAR(MAX) NOT NULL,
    [Prescription] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE [ReportTemplates] (
    [TemplateID] INT IDENTITY(1,1) PRIMARY KEY,
    [TemplateName] VARCHAR(100) NOT NULL,
    [IsActive] BIT DEFAULT 1
)
GO

CREATE TABLE [TemplateFields] (
    [FieldID] INT IDENTITY(1,1) PRIMARY KEY,
    [TemplateID] INT FOREIGN KEY REFERENCES [ReportTemplates]([TemplateID]),
    [FieldName] VARCHAR(100) NOT NULL,
    [FieldType] VARCHAR(20) NOT NULL CHECK ([FieldType] IN ('Text', 'Boolean', 'Numeric')),
    [MinValue] DECIMAL(18,2) NULL,
    [MaxValue] DECIMAL(18,2) NULL,
    [Unit] VARCHAR(20) NULL,
    [NormalBoolean] VARCHAR(20) NULL
)
GO

CREATE TABLE [ReportResults] (
    [ResultID] INT IDENTITY(1,1) PRIMARY KEY,
    [ReportID] INT NULL,
    [FieldID] INT FOREIGN KEY REFERENCES [TemplateFields]([FieldID]),
    [EncryptedValue] NVARCHAR(MAX) NOT NULL
)
GO

-- Default User
INSERT INTO [Users] ([Username], [Password], [Role]) VALUES ('doctor', 'password123', 'Doctor')
GO

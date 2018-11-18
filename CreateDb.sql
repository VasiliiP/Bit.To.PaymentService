Use [Master]
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'PaymentDB')
	BEGIN
		CREATE DATABASE [PaymentDB];
	END
GO
USE [PaymentDB]
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'puser')
	BEGIN
		CREATE LOGIN [puser] WITH PASSWORD = 'pay123'
		CREATE USER [puser] FOR LOGIN [puser]
		EXEC sp_addrolemember N'db_owner', N'puser'
	END
	

CREATE TABLE [dbo].[ReceiptItem](
	Id int IDENTITY(1,1) NOT NULL,
	ReceiptId uniqueidentifier NOT NULL,
	StatusCode int NOT NULL,
	StatusName nvarchar(20) NOT NULL,
	StatusMessage nvarchar(20) NOT NULL,
	ModifiedDateUtc datetime NOT NULL,
	ReceiptDateUtc datetime NOT NULL,
	InvoiceId nvarchar(50) NOT NULL,
	Inn nvarchar(50) NOT NULL,
	[Type] nvarchar(20) NOT NULL,
	CashboxId int NOT NULL,
	CustomerReceiptId int NOT NULL

PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
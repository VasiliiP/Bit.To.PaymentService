Use [Master]
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'PaymentDB')
	BEGIN
		CREATE DATABASE [PaymentDB];
	END
GO
USE [PaymentDB]
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'puser')
	BEGIN
		CREATE LOGIN [puser] WITH PASSWORD = 'pay123qaz!'
		CREATE USER [puser] FOR LOGIN [puser]
		EXEC sp_addrolemember N'db_owner', N'puser'
	END
	
IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Receipts'))
	DROP TABLE [dbo].[Receipts]
GO
CREATE TABLE [dbo].[Receipts](
	Id int IDENTITY(1,1) NOT NULL,
	[UID] uniqueidentifier NOT NULL,
	StatusCode int NOT NULL,
	StatusName nvarchar(20) NOT NULL,
	StatusMessage nvarchar(50) NOT NULL,
	Modified datetime NOT NULL,
	ReceiptDate datetime NOT NULL,
	InvoiceId nvarchar(50) NOT NULL,
	Inn nvarchar(50) NOT NULL,
	[Type] nvarchar(20) NOT NULL,
	CashboxId int NOT NULL,
	TaxSystem nvarchar(30) NOT NULL,
	Email nvarchar(30) NOT NULL,
	Phone nvarchar(30) NOT NULL,
	Iplace nvarchar(50) NULL,
	Iaddress nvarchar(50) NULL,
	Dnumber nvarchar(50) NULL,

PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Cashboxes'))
	DROP TABLE [dbo].[Cashboxes]
GO
CREATE TABLE [dbo].[Cashboxes](
	Id int IDENTITY(1,1) NOT NULL,
	DeviceId INT NOT NULL,
	Rnm nvarchar(30) NOT NULL,
	Zn nvarchar(30) NOT NULL,
	Fn nvarchar(30) NOT NULL,
	Fdn nvarchar(30) NOT NULL,
	Fpd nvarchar(30) NOT NULL,

PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'ReceiptItems'))
	DROP TABLE [dbo].[ReceiptItems]
GO
CREATE TABLE [dbo].[ReceiptItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptId] [int] NOT NULL,
	[Label] [nvarchar](max) NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[Quantity] [decimal](12, 2) NOT NULL,
	[Amount] [decimal](12, 2) NOT NULL,
	[Vat] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReceiptItems]  WITH NOCHECK ADD  CONSTRAINT [FK_ReceiptItems_Receipts] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[Receipts] ([Id])
GO

ALTER TABLE [dbo].[ReceiptItems] CHECK CONSTRAINT [FK_ReceiptItems_Receipts]
GO

ALTER TABLE [dbo].[Receipts]  WITH CHECK ADD  CONSTRAINT [FK_Receipts_Cashboxes] FOREIGN KEY([CashboxId])
REFERENCES [dbo].[Cashboxes] ([Id])
GO

ALTER TABLE [dbo].[Receipts] CHECK CONSTRAINT [FK_Receipts_Cashboxes]
GO

------------------------------------------------------------------------------------------
USE [PaymentDB]
GO

INSERT INTO [dbo].[Cashboxes]
           ([DeviceId]
           ,[Rnm]
           ,[Zn]
           ,[Fn]
           ,[Fdn]
           ,[Fpd])
     VALUES
           (113
           ,0000000113054100
           ,00107701222815
           ,9999078900011693
           ,0000043586
           ,3467424788)
GO


USE [PaymentDB]
GO

INSERT INTO [dbo].[Receipts]
           ([UID]
           ,[StatusCode]
           ,[StatusName]
           ,[StatusMessage]
           ,[Modified]
           ,[ReceiptDate]
           ,[InvoiceId]
           ,[Inn]
           ,[Type]
           ,[CashboxId]
           ,[TaxSystem]
           ,[Email]
           ,[Phone]
           ,[Iplace]
           ,[Iaddress]
           ,[Dnumber])
     VALUES
           (NEWID()
           ,2
           ,N'Чек передан в ОФД'
           ,N'CONFIRMED'
           ,CAST(N'2018-11-17 20:16:29.000' AS DateTime)
           ,CAST(N'2018-11-17 20:17:21.000' AS DateTime)
           ,N'InvoiceId100'
           ,N'2539112357'
           ,N'Income'
           , (SELECT TOP 1 Id FROM Cashboxes )
           ,N'Common'
           ,N'asd@dsa.ru'
           ,N'89991234567'
           ,NULL
           ,NULL
           ,NULL)
GO

INSERT INTO [dbo].[ReceiptItems]
           ([ReceiptId]
           ,[Label]
           ,[Price]
           ,[Quantity]
           ,[Amount]
           ,[Vat])
     VALUES
           ((SELECT TOP 1 Id FROM Receipts )
           ,N'Tomates'
           ,CAST(40.00 AS decimal(6,2))
           ,CAST(12.00 AS decimal(6,2))
           ,CAST(480.00 AS decimal(6,2))
           ,N'Vat10')
GO

INSERT INTO [dbo].[ReceiptItems]
           ([ReceiptId]
           ,[Label]
           ,[Price]
           ,[Quantity]
           ,[Amount]
           ,[Vat])
     VALUES
           ((SELECT TOP 1 Id FROM Receipts )
           ,N'Cucumbers'
           ,CAST(40.00 AS decimal(6,2))
           ,CAST(10.00 AS decimal(6,2))
           ,CAST(400.00 AS decimal(6,2))
           ,N'Vat10')
GO

SELECT R.Id ,R.UID ,R.StatusCode ,R.StatusName ,R.StatusMessage ,R.Modified ,R.ReceiptDate
      ,R.InvoiceId ,R.Inn ,R.Type ,R.CashboxId ,R.TaxSystem ,R.Email ,R.Phone ,R.Iplace ,R.Iaddress
      ,R.Dnumber, C.DeviceId, C.Rnm, C.Zn, C.Fn, C.Fdn, C.Fpd 
  FROM [dbo].[Receipts] R
  JOIN Cashboxes C ON C.Id = R.CashboxId
  WHERE R.Id = 1
IF DB_ID('VideoGame_Shop') IS NOT NULL
BEGIN
USE VideoGame_Shop
	IF  OBJECT_ID('dbo.Inventory','U') IS NOT NULL 
		BEGIN
		   DROP TABLE dbo.Inventory;
		END

	IF OBJECT_ID('dbo.Sales','U') IS NOT NULL 
		BEGIN
			DROP TABLE dbo.Sales;
		END

	IF OBJECT_ID('dbo.P_Platforms','U') IS NOT NULL 
		BEGIN
		   DROP TABLE dbo.P_Platforms;
		END

	IF OBJECT_ID('dbo.P_Categories','U') IS NOT NULL 
		BEGIN
		   DROP TABLE dbo.P_Categories;
		END

	IF OBJECT_ID('dbo.P_Conditions','U') IS NOT NULL 
	BEGIN
		DROP TABLE dbo.P_Conditions;
	END

	IF OBJECT_ID('dbo.P_Types','U') IS NOT NULL 
	BEGIN
		DROP TABLE dbo.P_Types;
	END

	IF OBJECT_ID('dbo.Roles','U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.Roles;
	END

	IF OBJECT_ID('dbo.Users','U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.Users;
	END

	IF OBJECT_ID('dbo.UserRoles','U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.UserRoles;
	END
END

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'dbo.spCreateCashOrder') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
	DROP PROCEDURE dbo.spCreateCashOrder;
END

IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'dbo.spCreateCreditCardOrder') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
	DROP PROCEDURE dbo.spCreateCreditCardOrder;
END

IF DB_ID('VideoGame_Shop') IS NULL

	BEGIN
		CREATE DATABASE VideoGame_Shop
	END


USE Videogame_Shop
GO

CREATE TABLE Inventory (productId INTEGER CONSTRAINT PproductKey PRIMARY KEY IDENTITY(1,1), 
	[Game Title] varchar(50), 
	Category varchar(50), 
	Platform varchar(50), 
	[Available Units] int,
	Cost money, 
	Price money, 
	Condition varchar(50), 
	[Product Type] varchar(50)
)

CREATE TABLE Sales (orderId INTEGER CONSTRAINT PsalesKey PRIMARY KEY IDENTITY(1,1),
	Product varchar(50), 
	Quantity int, 
	Condition varchar(50), 
	Date date, 
	Total money, 
	[Customer Name] varchar(50), 
	[Customer Phone] varchar(30), 
	Email varchar(50),
	[Sale Type] varchar(10), 
	[Credit Card Name] varchar(50), 
	[Credit Card Number] bigint, 
	[Expiration Date] date,
	[Security Code] varchar(5)
)

CREATE TABLE P_Categories(
	Category varchar(50) UNIQUE , 
) 
CREATE TABLE P_Platforms(
	Platform varchar(50) UNIQUE, 
)
CREATE TABLE P_Conditions(
	Condition varchar(50) UNIQUE, 
)
CREATE TABLE P_Types(
	[Product Type] varchar(50) UNIQUE
)
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL DEFAULT(0),
	[CreatedOnDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedOnDate]  DEFAULT (getdate()) FOR [CreatedOnDate]
GO

CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [nvarchar](50) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
	[RoleID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE Inventory
ADD FOREIGN KEY(Category)
REFERENCES P_Categories(Category) 
ON DELETE NO ACTION;

ALTER TABLE Inventory
ADD FOREIGN KEY(Platform)
REFERENCES P_Platforms(Platform)
ON DELETE NO ACTION;

ALTER TABLE Inventory
ADD FOREIGN KEY(Condition)
REFERENCES P_Conditions(Condition)
ON DELETE NO ACTION;

ALTER TABLE Inventory
ADD FOREIGN KEY([Product Type])
REFERENCES P_Types([Product Type])
ON DELETE NO ACTION;

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

	IF OBJECT_ID('dbo.AppUser','U') IS NOT NULL
	BEGIN
		DROP TABLE dbo.AppUser;
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
	GameTitle varchar(50), 
	Category varchar(50), 
	Platform varchar(50), 
	AvailableUnits int,
	Cost money, 
	Price money, 
	Condition varchar(50), 
	ProductType varchar(50)
)

CREATE TABLE Sales (orderId INTEGER CONSTRAINT PsalesKey PRIMARY KEY IDENTITY(1,1),
	Product varchar(50), 
	Quantity int, 
	Condition varchar(50), 
	Date date, 
	Total money, 
	CustomerName varchar(50), 
	CustomerPhoneNumber varchar(30), 
	Email varchar(50),
	SaleType varchar(10), 
	CreditCardName varchar(50), 
	CreditCardNumber varchar(4),
	EncryptedCreditCardNumber varchar(50),
	ExpirationDate date,
	SecurityCode varchar(5)
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
	ProductType varchar(50) UNIQUE
)
CREATE TABLE AppUser(UserId INTEGER CONSTRAINT PappUserKey PRIMARY KEY IDENTITY(1,1),
	UserName varchar(50) UNIQUE,
	Password varchar(50),
	Role varchar(50)
)

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
ADD FOREIGN KEY(ProductType)
REFERENCES P_Types(ProductType)
ON DELETE NO ACTION;

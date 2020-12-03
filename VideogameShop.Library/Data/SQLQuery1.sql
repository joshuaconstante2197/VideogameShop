use [master];
IF DB_ID('VideoGame_Shop') IS NOT NULL
BEGIN
	ALTER DATABASE VideoGame_Shop SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE VideoGame_Shop SET ONLINE;
	DROP DATABASE VideoGame_Shop;
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
CREATE TABLE Role(RoleId INTEGER CONSTRAINT ProleKey PRIMARY KEY IDENTITY(1,1),
	RoleName varchar(50) UNIQUE
)

CREATE TABLE UserRole(UserId int,
FOREIGN KEY(UserId) REFERENCES AppUser(UserId),
RoleId int,
FOREIGN KEY(RoleId) REFERENCES Role(RoleId)
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

GO

create procedure dbo.spCreateCashOrder
		@Product varchar(50), 
		@Quantity int, 
		@Condition varchar(50), 
		@Date date, 
		@Total money, 
		@CustomerName varchar(50),
		@CustomerPhoneNumber varchar(30), 
		@Email varchar(50), 
		@SaleType varchar(10)
as
begin 
		set nocount on;
		insert into Sales(Product, 
						Quantity, 
						Condition, 
						Date, 
						Total, 
						CustomerName, 
						CustomerPhoneNumber,
						Email, 
						SaleType)
		values(@Product, 
				@Quantity, 
				@Condition, 
				@Date, 
				@Total, 
				@CustomerName,
				@CustomerPhoneNumber, 
				@Email, 
				@SaleType);
		UPDATE Inventory SET AvailableUnits = AvailableUnits - @Quantity WHERE GameTitle = @Product;
end

GO

create procedure dbo.spCreateCreditCardOrder
		@Product varchar(50), 
		@Quantity int, 
		@Condition varchar(50), 
		@Date date, 
		@Total money, 
		@CustomerName varchar(50),
		@CustomerPhoneNumber varchar(30), 
		@Email varchar(50), 
		@SaleType varchar(10),
		@CreditCardName varchar(50),
		@CreditCardNumber varchar(4),
		@EncryptedCreditCardNumber varchar(50),
		@ExpirationDate date,
		@SecurityCode varchar(5)
as
begin 
		set nocount on;
		insert into Sales(Product, 
						Quantity, 
						Condition, 
						Date, 
						Total, 
						CustomerName, 
						CustomerPhoneNumber,
						Email, 
						SaleType,
                        CreditCardName, 
						CreditCardNumber,
						EncryptedCreditCardNumber,
						ExpirationDate, 
						SecurityCode)
		values(@Product, 
				@Quantity, 
				@Condition, 
				@Date, 
				@Total, 
				@CustomerName,
				@CustomerPhoneNumber, 
				@Email, 
				@SaleType,
				@CreditCardName,
				@CreditCardNumber,
				@EncryptedCreditCardNumber,
				@ExpirationDate,
				@SecurityCode);
		UPDATE Inventory SET AvailableUnits = AvailableUnits - @Quantity WHERE GameTitle = @Product;
end

Go
INSERT INTO Role(RoleName) VALUES('admin'),('employee');


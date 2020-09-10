DROP TABLE dbo.Inventory;
DROP TABLE dbo.P_Platforms;
DROP TABLE dbo.P_Categories;
DROP TABLE dbo.P_Conditions;
DROP TABLE dbo.P_Types;
DROP TABLE dbo.Sales;

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
	Date date, Total money, 
	[Customer Name] varchar(50), 
	[Customer Phone] varchar(30), 
	Email varchar(50),
	[Type of Sale] varchar(10), 
	[Name on Credit Card] varchar(50), 
	[Credit Card Number] int, 
	[Expiration Date] date,
	[Security Code] int
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

--INSERT INTO P_Categories(Category) VALUES('Action')
--INSERT INTO P_Categories(Category) VALUES('Adventure')
--INSERT INTO P_Platforms(Platform) VALUES('ps$')


--INSERT INTO P_Categories(Category) SELECT('Action') WHERE NOT EXISTS(SELECT * FROM P_Categories WHERE Category = 'Action')

SELECT * FROM P_Categories
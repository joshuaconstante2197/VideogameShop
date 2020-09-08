CREATE TABLE Inventory (productId INTEGER CONSTRAINT PproductKey PRIMARY KEY IDENTITY(1,1), 
[Game Title] varchar(50), Category varchar(50), Platform varchar(50), [Available Units] int,
Cost money, Price money, Condition varchar(50), [Product Type] varchar(50))

CREATE TABLE Sales (orderId INTEGER CONSTRAINT PsalesKey PRIMARY KEY IDENTITY(1,1),
Product varchar(50), Quantity int, Condition varchar(50), Date date, Total money, [Customer Name] varchar(50), [Customer Phone] varchar(30), Email varchar(50),
[Type of Sale] varchar(10), [Name on Credit Card] varchar(50), [Credit Card Number] int, [Expiration Date] date,[Security Code] int)


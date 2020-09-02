CREATE TABLE Inventory (productId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY, 
[Game Title] varchar(50), Category varchar(50), Platform varchar(50), [Available Units] int,
Cost money, Price money, Condition varchar(50), [Product Type] varchar(50))

CREATE TABLE Sales (orderId INTEGER CONSTRAINT PsalesKey PRIMARY KEY,
Product varchar(50), Condition varchar(50), Date date, Total money, [Customer Name] varchar(50), [Customer Phone] varchar(30), Email varchar(50))
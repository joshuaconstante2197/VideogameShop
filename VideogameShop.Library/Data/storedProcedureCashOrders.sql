USE [Videogame_Shop]
GO

create procedure dbo.spCreateCashOrder
		@Product varchar(50), 
		@Quantity int, 
		@Condition varchar(50), 
		@Date date, 
		@Total money, 
		@CustomerName varchar(50),
		@CustomerName varchar(30), 
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
						CustomerName,
						Email, 
						SaleType)
		values(@Product, 
				@Quantity, 
				@Condition, 
				@Date, 
				@Total, 
				@CustomerName,
				@CustomerName, 
				@Email, 
				@SaleType);
		UPDATE Inventory SET AvailableUnits = AvailableUnits - @Quantity WHERE GameTitle = @Product;
end
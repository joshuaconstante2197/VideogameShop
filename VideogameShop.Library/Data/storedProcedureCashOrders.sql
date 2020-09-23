create procedure dbo.spCreateCashOrder
		@Product varchar(50), 
		@Quantity int, 
		@Condition varchar(50), 
		@Date date, 
		@Total money, 
		@CustomerName varchar(50),
		@CustomerPhone varchar(30), 
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
						[Customer Name], 
						[Customer Phone],
						Email, 
						[Sale Type])
		values(@Product, 
				@Quantity, 
				@Condition, 
				@Date, 
				@Total, 
				@CustomerName,
				@CustomerPhone, 
				@Email, 
				@SaleType);
		UPDATE Inventory SET [Available Units] = [Available Units] - @Quantity WHERE [Game Title] = @Product;
end
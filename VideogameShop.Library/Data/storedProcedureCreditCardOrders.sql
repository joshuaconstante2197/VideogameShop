USE [Videogame_Shop]
GO

create procedure dbo.spCreateCreditCardOrder
		@Product varchar(50), 
		@Quantity int, 
		@Condition varchar(50), 
		@Date date, 
		@Total money, 
		@CustomerName varchar(50),
		@CustomerPhone varchar(30), 
		@Email varchar(50), 
		@SaleType varchar(10),
		@CreditCardName varchar(50),
		@CreditCardNumber bigint,
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
						[Customer Name], 
						[Customer Phone],
						Email, 
						[Sale Type],
                        [Credit Card Name], 
						[Credit Card Number], 
						[Expiration Date], 
						[Security Code])
		values(@Product, 
				@Quantity, 
				@Condition, 
				@Date, 
				@Total, 
				@CustomerName,
				@CustomerPhone, 
				@Email, 
				@SaleType,
				@CreditCardName,
				@CreditCardNumber,
				@ExpirationDate,
				@SecurityCode);
		UPDATE Inventory SET [Available Units] = [Available Units] - @Quantity WHERE [Game Title] = @Product;
end

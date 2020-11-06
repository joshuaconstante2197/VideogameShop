USE [Videogame_Shop]
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
						CustomerName, 
						CustomerPhoneNumber,
						Email, 
						SaleType,
                        CreditCardName, 
						CreditCardNumber, 
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
				@ExpirationDate,
				@SecurityCode);
		UPDATE Inventory SET AvailableUnits = AvailableUnits - @Quantity WHERE GameTitle = @Product;
end

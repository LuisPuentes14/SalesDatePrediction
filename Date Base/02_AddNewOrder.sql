USE [StoreSample]
GO

/****** Object:  StoredProcedure [Sales].[AddNewOrder]    Script Date: 1/09/2025 12:51:04 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [Sales].[AddNewOrder]
    @CustId INT,
    @EmpId INT,
    @OrderDate DATETIME,
    @RequiredDate DATETIME,
    @ShippedDate DATETIME = NULL,
    @ShipperId INT,
    @Freight MONEY,
    @ShipName NVARCHAR(40),
    @ShipAddress NVARCHAR(60),
    @ShipCity NVARCHAR(15),
    @ShipCountry NVARCHAR(15),

    -- Detalle del producto
    @ProductId INT,
    @UnitPrice MONEY,
    @Qty SMALLINT,
    @Discount NUMERIC(4,3) = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewOrderId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insertar en Orders
        INSERT INTO Sales.Orders (
            custid, empid, orderdate, requireddate, shippeddate,
            shipperid, freight, shipname, shipaddress, shipcity,
             shipcountry
        )
        VALUES (
            @CustId, @EmpId, @OrderDate, @RequiredDate, @ShippedDate,
            @ShipperId, @Freight, @ShipName, @ShipAddress, @ShipCity,
             @ShipCountry
        );

        SET @NewOrderId = SCOPE_IDENTITY();

        -- Insertar en OrderDetails
        INSERT INTO Sales.OrderDetails (orderid, productid, unitprice, qty, discount)
        VALUES (@NewOrderId, @ProductId, @UnitPrice, @Qty, @Discount);

        COMMIT TRANSACTION;

        -- Retornar el ID de la nueva orden
        SELECT @NewOrderId AS NewOrderId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO



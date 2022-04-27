CREATE TABLE [dbo].[carts_pizzas] (
    [pizza_id] INT NOT NULL,
    [cart_id]  INT NOT NULL,
    CONSTRAINT [FK_carts_pizzas_carts] FOREIGN KEY ([cart_id]) REFERENCES [dbo].[carts] ([id]),
    CONSTRAINT [FK_carts_pizzas_pizzas] FOREIGN KEY ([pizza_id]) REFERENCES [dbo].[pizzas] ([id])
);


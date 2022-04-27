CREATE TABLE [dbo].[pizzas_ingredients] (
    [ingredient_id] INT NOT NULL,
    [pizza_id]      INT NOT NULL,
    CONSTRAINT [FK_pizzas_ingredients_ingredients] FOREIGN KEY ([ingredient_id]) REFERENCES [dbo].[ingredients] ([id]),
    CONSTRAINT [FK_pizzas_ingredients_pizzas] FOREIGN KEY ([pizza_id]) REFERENCES [dbo].[pizzas] ([id])
);


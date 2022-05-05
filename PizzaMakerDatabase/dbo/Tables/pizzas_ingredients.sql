CREATE TABLE [dbo].[pizzas_ingredients] (
    [id]            BIGINT IDENTITY (1, 1) NOT NULL,
    [ingredient_id] INT    NOT NULL,
    [pizza_id]      INT    NOT NULL,
    CONSTRAINT [PK_pizzas_ingredients_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_pizzas_ingredients_ingredients] FOREIGN KEY ([ingredient_id]) REFERENCES [dbo].[ingredients] ([id]),
    CONSTRAINT [FK_pizzas_ingredients_pizzas] FOREIGN KEY ([pizza_id]) REFERENCES [dbo].[pizzas] ([id])
);




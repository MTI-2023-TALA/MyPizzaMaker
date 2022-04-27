CREATE TABLE [dbo].[pizzas] (
    [id]   INT          IDENTITY (1, 1) NOT NULL,
    [name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_pizzas] PRIMARY KEY CLUSTERED ([id] ASC)
);


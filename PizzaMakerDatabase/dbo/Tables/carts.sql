CREATE TABLE [dbo].[carts] (
    [id]     INT          IDENTITY (1, 1) NOT NULL,
    [status] VARCHAR (50) NOT NULL,
    [date]   DATE         NOT NULL,
    CONSTRAINT [PK_carts] PRIMARY KEY CLUSTERED ([id] ASC)
);


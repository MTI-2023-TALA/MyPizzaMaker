CREATE TABLE [dbo].[ingredients] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [name]         VARCHAR (50)  NOT NULL,
    [image_path]   VARCHAR (250) NULL,
    [is_available] BIT           NOT NULL,
    [category]     VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_ingredients] PRIMARY KEY CLUSTERED ([id] ASC)
);


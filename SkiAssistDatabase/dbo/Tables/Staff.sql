CREATE TABLE [dbo].[Staff] (
    [staffId]           INT           NOT NULL,
    [staffFirstName]    NVARCHAR (50) NOT NULL,
    [staffLastName]     NVARCHAR (50) NOT NULL,
    [staffContactNum]   NVARCHAR (50) NOT NULL,
    [staffContactEmail] NVARCHAR (50) NOT NULL,
    [staffUsername]     NVARCHAR (50) NOT NULL,
    [roleType]          NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([staffId] ASC),
    CONSTRAINT [FK_Staff_Role] FOREIGN KEY ([roleType]) REFERENCES [dbo].[Role] ([roleType])
);


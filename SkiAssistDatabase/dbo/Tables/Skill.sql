CREATE TABLE [dbo].[Skill] (
    [skillTitle]     NVARCHAR (50) NOT NULL,
    [skillLevel]     INT           NULL,
    [skillSortOrder] INT           NULL,
    PRIMARY KEY CLUSTERED ([skillTitle] ASC)
);


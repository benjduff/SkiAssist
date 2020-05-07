CREATE TABLE [dbo].[PermissionGroup] (
    [roleType]   NVARCHAR (50) NOT NULL,
    [permission] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED ([roleType] ASC, [permission] ASC),
    CONSTRAINT [FK_PermissionGroup_Permission] FOREIGN KEY ([permission]) REFERENCES [dbo].[Permission] ([permission]),
    CONSTRAINT [FK_PermissionGroup_Role] FOREIGN KEY ([roleType]) REFERENCES [dbo].[Role] ([roleType])
);


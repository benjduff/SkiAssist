CREATE TABLE [dbo].[Guardian] (
    [guardianID]            INT           NOT NULL,
    [guardianFirstName]     NVARCHAR (50) NULL,
    [guardianLastName]      NVARCHAR (50) NULL,
    [guardianContactNumber] NVARCHAR (50) NULL,
    [guardianContactEmail]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([guardianID] ASC)
);


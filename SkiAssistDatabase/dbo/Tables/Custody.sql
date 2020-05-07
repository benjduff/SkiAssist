CREATE TABLE [dbo].[Custody] (
    [studentId]        INT           NOT NULL,
    [staffId]          INT           NOT NULL,
    [custodyStartTime] DATETIME      NOT NULL,
    [custodyEndTime]   DATETIME      NULL,
    [custodyType]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Custody] PRIMARY KEY CLUSTERED ([custodyStartTime] ASC, [staffId] ASC, [studentId] ASC),
    CONSTRAINT [FK_Custody_CustodyType] FOREIGN KEY ([custodyType]) REFERENCES [dbo].[CustodyType] ([custodyType]),
    CONSTRAINT [FK_Custody_Staff] FOREIGN KEY ([staffId]) REFERENCES [dbo].[Staff] ([staffId]),
    CONSTRAINT [FK_Custody_Student] FOREIGN KEY ([studentId]) REFERENCES [dbo].[Student] ([studentId])
);


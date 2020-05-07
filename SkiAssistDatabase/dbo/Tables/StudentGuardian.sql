CREATE TABLE [dbo].[StudentGuardian] (
    [studentId]                   INT           NOT NULL,
    [guardianId]                  INT           NOT NULL,
    [studentguardianRelationship] NVARCHAR (50) NULL,
    [studentguardianExpiry]       DATE          NULL,
    PRIMARY KEY CLUSTERED ([guardianId] ASC, [studentId] ASC),
    CONSTRAINT [FK_StudentGuardian_Guardian] FOREIGN KEY ([guardianId]) REFERENCES [dbo].[Guardian] ([guardianID]),
    CONSTRAINT [FK_StudentGuardian_Student] FOREIGN KEY ([studentId]) REFERENCES [dbo].[Student] ([studentId])
);


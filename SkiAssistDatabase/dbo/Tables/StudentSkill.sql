CREATE TABLE [dbo].[StudentSkill] (
    [studentId]         INT           NOT NULL,
    [skillTitle]        NVARCHAR (50) NOT NULL,
    [isPassed]          TINYINT       NULL,
    [studentskillNotes] NVARCHAR (50) NULL,
    CONSTRAINT [PK_StudentSkill] PRIMARY KEY CLUSTERED ([skillTitle] ASC, [studentId] ASC),
    CONSTRAINT [FK_StudentSkill_Skill] FOREIGN KEY ([skillTitle]) REFERENCES [dbo].[Skill] ([skillTitle]),
    CONSTRAINT [FK_StudentSkill_Student] FOREIGN KEY ([studentId]) REFERENCES [dbo].[Student] ([studentId])
);


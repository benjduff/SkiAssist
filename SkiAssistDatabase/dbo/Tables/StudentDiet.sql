CREATE TABLE [dbo].[StudentDiet] (
    [dietType]  NVARCHAR (50) NOT NULL,
    [studentId] INT           NOT NULL,
    CONSTRAINT [PK_StudentDiet] PRIMARY KEY CLUSTERED ([studentId] ASC, [dietType] ASC),
    CONSTRAINT [FK_StudentDiet_Diet] FOREIGN KEY ([dietType]) REFERENCES [dbo].[Diet] ([dietType]),
    CONSTRAINT [FK_StudentDiet_Student] FOREIGN KEY ([studentId]) REFERENCES [dbo].[Student] ([studentId])
);


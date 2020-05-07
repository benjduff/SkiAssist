CREATE TABLE [dbo].[Student] (
    [studentId]                   INT           NOT NULL,
    [studentFirstName]            NVARCHAR (50) NOT NULL,
    [studentLastName]             NVARCHAR (50) NOT NULL,
    [studentDOB]                  DATE          NOT NULL,
    [studentEmergencyContactNum]  NVARCHAR (50) NULL,
    [studentEmergencyContactName] NVARCHAR (50) NULL,
    [ticketNumber]                INT           NULL,
    CONSTRAINT [pk_student] PRIMARY KEY CLUSTERED ([studentId] ASC),
    CONSTRAINT [FK_Student_Ticket] FOREIGN KEY ([ticketNumber]) REFERENCES [dbo].[Ticket] ([ticketNumber])
);


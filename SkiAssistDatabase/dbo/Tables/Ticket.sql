CREATE TABLE [dbo].[Ticket] (
    [ticketNumber]           INT     NOT NULL,
    [ticketIssueDate]        DATE    NULL,
    [ticketExpiryDate]       DATE    NULL,
    [ticketLessonsRemaining] INT     NULL,
    [ticketIsValid]          TINYINT NULL,
    CONSTRAINT [pk_ticket] PRIMARY KEY CLUSTERED ([ticketNumber] ASC)
);


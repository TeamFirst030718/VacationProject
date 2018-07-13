CREATE TABLE [dbo].[Vacation] (
    [VacationID]           NVARCHAR (128) NOT NULL,
    [EmployeeID]           NVARCHAR (128) NOT NULL,
    [DateOfBegin]          DATE           NOT NULL,
    [DateOfEnd]            DATE           NOT NULL,
    [Comment]              NVARCHAR (100) NULL,
    [VacationStatusTypeID] NVARCHAR (128) NOT NULL,
    [TransactionID]        NVARCHAR (128) NULL,
    CONSTRAINT [PK__Vacation__E420DF844B0C6CC3] PRIMARY KEY CLUSTERED ([VacationID] ASC),
    CONSTRAINT [FK_Vacation_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]),
    CONSTRAINT [FK_Vacation_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [dbo].[Transaction] ([TransactionID]),
    CONSTRAINT [FK_Vacation_VacationStatusType] FOREIGN KEY ([VacationStatusTypeID]) REFERENCES [dbo].[VacationStatusType] ([VacationStatusTypeID])
);


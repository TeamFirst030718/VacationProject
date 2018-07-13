CREATE TABLE [dbo].[Transaction] (
    [TransactionID]     NVARCHAR (128) NOT NULL,
    [BalanceChange]     INT            NOT NULL,
    [Discription]       NVARCHAR (50)  NOT NULL,
    [EmployeeID]        NVARCHAR (128) NOT NULL,
    [TransactionDate]   DATE           NOT NULL,
    [TransactionTypeID] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK__Transact__55433A4B94A0FB53] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeID]) REFERENCES [dbo].[TransactionType] ([TransactionTypeID]),
    CONSTRAINT [FK_Transactions_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);


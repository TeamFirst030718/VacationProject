CREATE TABLE [dbo].[Team] (
    [TeamID]     NVARCHAR (128) NOT NULL,
    [TeamName]   NVARCHAR (50)  NOT NULL,
    [TeamLeadID] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK__Teams__123AE7B98B6D3F10] PRIMARY KEY CLUSTERED ([TeamID] ASC),
    CONSTRAINT [FK_Teams_Employees] FOREIGN KEY ([TeamLeadID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);


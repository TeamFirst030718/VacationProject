CREATE TABLE [dbo].[EmployeeTeam] (
    [EmployeeID] NVARCHAR (128) NOT NULL,
    [TeamID]     NVARCHAR (128) NOT NULL,
    CONSTRAINT [FK_EmployeesTeams_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]),
    CONSTRAINT [FK_EmployeesTeams_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Team] ([TeamID])
);


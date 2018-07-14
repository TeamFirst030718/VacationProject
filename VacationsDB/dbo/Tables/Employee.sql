CREATE TABLE [dbo].[Employee] (
    [EmployeeID]      NVARCHAR (128) NOT NULL,
    [Name]            NVARCHAR (20)  NOT NULL,
    [Surname]         NVARCHAR (30)  NOT NULL,
    [BirthDate]       DATE           NOT NULL,
    [PersonalMail]    NVARCHAR (256) NOT NULL,
    [Skype]           NVARCHAR (60)  NOT NULL,
    [HireDate]        DATE           NOT NULL,
    [Status]          BIT            NOT NULL,
    [DateOfDismissal] DATE           NULL,
    [VacationBalance] INT            NOT NULL,
    [JobTitleID]      NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK__Employee__7AD04FF188E2F61A] PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    CONSTRAINT [FK_Employee_AspNetUsers] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Employees_Positions] FOREIGN KEY ([JobTitleID]) REFERENCES [dbo].[JobTitle] ([JobTitleID])
);


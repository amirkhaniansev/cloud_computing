CREATE TABLE [dbo].[Salaries]
(
	[Id]	          INT IDENTITY(1, 1)  NOT NULL PRIMARY KEY,
	[CreationDate]    DATETIME            NOT NULL,
    [Company]         VARCHAR(50)         NOT NULL,
    [Position]        VARCHAR(50)         NOT NULL,
    [Salary]          INT                 NOT NULL,
    [Experience]      INT                 NOT NULL
)

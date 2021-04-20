CREATE TABLE [dbo].[Salaries]
(
	[Id]	          INT IDENTITY(1, 1)  NOT NULL PRIMARY KEY,
	[CreationDate]    DATETIME2           NOT NULL,
    [Company]         VARCHAR(80)         NULL,
    [Position]        VARCHAR(80)         NOT NULL,
    [Salary]          INT                 NOT NULL,
    [Experience]      FLOAT               NOT NULL
)

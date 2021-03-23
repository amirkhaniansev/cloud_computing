CREATE TABLE [dbo].[TrafficJam]
(
	[Id]		INT	IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Degree]	INT,
	[Street]	NVARCHAR(255),
	[StartLocation]	GEOGRAPHY NULL,
	[EndLocation]	GEOGRAPHY NULL
)

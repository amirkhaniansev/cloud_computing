﻿CREATE TABLE [dbo].[Country]
(
	[Id]		INT		NOT NULL	IDENTITY(1, 1),
	[Created]	DATETIME	NOT NULL,
	[Modified]	DATETIME	NOT NULL,
	[Name]		NVARCHAR(255)	NOT NULL,
	[FlagURL]	NVARCHAR(4000)	NULL,

	CONSTRAINT [PK_COUNTRY_ID]	PRIMARY KEY ([Id])
)
﻿CREATE TABLE [dbo].[Season]
(
	[Id]		INT		NOT NULL	IDENTITY(1, 1),
	[Created]	DATETIME	NOT NULL,
	[Modified]	DATETIME	NOT NULL,
	[StartTime]	DATETIME	NOT NULL,
	[EndTime]	DATETIME	NOT NULL,
	[Description]	NVARCHAR(255)	NULL,
	[CompetitionId]	INT		NOT NULL,

	CONSTRAINT [PK_SEASON_ID]		PRIMARY KEY ([Id]),
	CONSTRAINT [FK_SEASON_COMPETITION_ID]	FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition]([Id])
)
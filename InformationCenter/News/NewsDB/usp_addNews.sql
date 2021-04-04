CREATE PROCEDURE [dbo].[usp_addNews]
	@title NVARCHAR(255),
	@author NVARCHAR(255),
	@category NVARCHAR(255),
	@content NVARCHAR(MAX),
	@created DATETIME2,
	@fileurl NVARCHAR(MAX) null
AS
	INSERT INTO [dbo].[News] VALUES (@title, @author, @category, @content, @created, @fileurl)
RETURN SCOPE_IDENTITY()

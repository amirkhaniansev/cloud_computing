CREATE PROCEDURE [dbo].[usp_getNews]
	@id INT
AS
	SELECT * FROM [dbo].[News] WHERE Id = @id
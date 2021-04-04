CREATE PROCEDURE [dbo].[usp_deleteNews]
	@id INT
AS
	DELETE FROM [dbo].[News] WHERE Id = @id
CREATE PROCEDURE [dbo].[usp_deleteFilmById]
	@id INT
AS
	DELETE FROM [dbo].[Film] WHERE Id = @id
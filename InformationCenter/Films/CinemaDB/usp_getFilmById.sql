CREATE PROCEDURE [dbo].[usp_getFilmById]
	@id INT
AS
	SELECT	*
	FROM [dbo].[Film]
	WHERE Id = @id

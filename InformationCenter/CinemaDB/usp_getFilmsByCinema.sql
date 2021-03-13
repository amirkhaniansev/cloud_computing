CREATE PROCEDURE [dbo].[usp_getFilmsByCinema]
	@cinema NVARCHAR(255)
AS
	SELECT	Id,Name,Stars,Category
	FROM [dbo].[Film]
	WHERE Cinema = @cinema

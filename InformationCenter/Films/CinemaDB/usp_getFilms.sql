CREATE PROCEDURE [dbo].[usp_getFilms]
AS
	SELECT	C.Address as CinemaAddress, F.*
	FROM [dbo].[Film] as F join [dbo].[Cinema] as C  on F.Cinema = C.Name
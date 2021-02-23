CREATE PROCEDURE [dbo].[usp_getTrafficJamById]
	@id INT
AS
	SELECT * FROM [dbo].[TrafficJam] WHERE Id = @id
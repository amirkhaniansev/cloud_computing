CREATE PROCEDURE [dbo].[usp_deleteTrafficJamById]
	@id INT
AS
	DELETE FROM [dbo].[TrafficJam] WHERE Id = @id
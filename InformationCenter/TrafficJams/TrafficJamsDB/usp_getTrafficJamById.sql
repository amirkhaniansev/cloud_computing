CREATE PROCEDURE [dbo].[usp_getTrafficJamById]
	@id INT
AS
	SELECT	Id,
		Degree,
		Street,
		StartLocation.Long as StartLocationLong,
		StartLocation.Lat  as StartLocationLat,
		EndLocation.Long   as EndLocationLong,
		EndLocation.Lat    as EndLocationLat
	FROM [dbo].[TrafficJam]
	WHERE Id = @id
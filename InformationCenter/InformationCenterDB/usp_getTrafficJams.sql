CREATE PROCEDURE [dbo].[usp_getTrafficJams]
AS
	SELECT	Id,
		Degree,
		Street,
		StartLocation.Long as StartLocationLong,
		StartLocation.Lat  as StartLocationLat,
		EndLocation.Long   as EndLocationLong,
		EndLocation.Lat    as EndLocationLat
	FROM [dbo].[TrafficJam]
CREATE PROCEDURE [dbo].[usp_getTrafficJamAddress]
	@Street NVARCHAR
AS
	If exists(SELECT * from TrafficJam where Street = @Street)
		return 1
	Else
		return 0
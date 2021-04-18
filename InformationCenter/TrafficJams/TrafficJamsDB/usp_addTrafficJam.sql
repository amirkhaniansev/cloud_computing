CREATE PROCEDURE [dbo].[usp_addTrafficJam]
	@degree INT,
	@street NVARCHAR(255),
	@startLocation GEOGRAPHY = NULL,
	@endLocation   GEOGRAPHY = NULL
AS
	INSERT INTO [dbo].[TrafficJam] VALUES (@degree, @street, @startLocation, @endLocation)
RETURN SCOPE_IDENTITY()
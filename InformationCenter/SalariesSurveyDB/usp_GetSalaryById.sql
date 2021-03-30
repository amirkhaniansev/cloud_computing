CREATE PROCEDURE [dbo].[usp_GetSalaryById]
	@Id		INT
AS
	SELECT *
	FROM [dbo].[Salaries]
	WHERE Id = @Id

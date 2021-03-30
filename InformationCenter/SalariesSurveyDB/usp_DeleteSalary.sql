CREATE PROCEDURE [dbo].[usp_DeleteSalary]
	@Id		INT
AS
	DELETE
	FROM [dbo].[Salaries]
	WHERE Id = @Id

CREATE PROCEDURE [dbo].[usp_AddSalary]
    @Company        VARCHAR(50),
    @Position       VARCHAR(50),
    @Salary         INT,
    @Experience     INT
AS
	INSERT INTO [dbo].[Salaries] VALUES (CURRENT_TIMESTAMP, @Company, @Position, @Salary, @Experience)
RETURN SCOPE_IDENTITY()

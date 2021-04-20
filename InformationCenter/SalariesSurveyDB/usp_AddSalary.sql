CREATE PROCEDURE [dbo].[usp_AddSalary]
    @Company        VARCHAR(80),
    @Position       VARCHAR(80),
    @Salary         INT,
    @Experience     FLOAT
AS
	INSERT INTO [dbo].[Salaries] VALUES (CURRENT_TIMESTAMP, @Company, @Position, @Salary, @Experience)
RETURN SCOPE_IDENTITY()

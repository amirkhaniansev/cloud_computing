CREATE PROCEDURE [dbo].[usp_addFilm]
	@Name		NVARCHAR(255),
	@Stars		NVARCHAR(500) = null,
	@Category	NVARCHAR(50),
    @Cinema	NCHAR(10)
AS
	INSERT INTO [dbo].[Film] VALUES (@Name, @Stars, @Category, @Cinema)
RETURN SCOPE_IDENTITY()
DROP PROCEDURE IF EXISTS UpdateRecordsNoAccess;
GO
CREATE PROCEDURE UpdateRecordsNoAccess
    @UserId NVARCHAR(MAX),
    @ExecuteAs NVARCHAR(MAX),
    @FullName NVARCHAR(MAX)
AS
BEGIN

    DECLARE @DynamicSQL NVARCHAR(MAX);

    IF @ExecuteAs = 'DefaultUser'
    BEGIN
        SET @DynamicSQL = 'EXECUTE AS USER = ''DefaultUser'';';
    END
    ELSE
    BEGIN
        SET @DynamicSQL = 'EXECUTE AS USER = ''HelpDesk'';';
    END;

    EXEC sp_executesql @DynamicSQL;
    
    UPDATE AspNetUsers
    SET UserName = @FullName
    WHERE Id = @UserId

    REVERT;

END

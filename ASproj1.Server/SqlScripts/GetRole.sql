DROP PROCEDURE IF EXISTS GetRole;
GO

CREATE PROCEDURE GetRole
    @UserId NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Role NVARCHAR(50)

    SELECT @Role = Patients.Role
    FROM Patients 
    WHERE Patients.PatientId = @UserId;

    SELECT @Role AS Role;
END

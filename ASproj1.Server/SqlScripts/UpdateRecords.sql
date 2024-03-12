DROP PROCEDURE UpdateRecords;
GO
CREATE PROCEDURE UpdateRecords
    @UserId NVARCHAR(MAX),
    @PhoneNumber NVARCHAR(MAX),
    @TreatmentPlan NVARCHAR(MAX),
    @DiagnosisDetails NVARCHAR(MAX),
    @FullName NVARCHAR(MAX)
AS
BEGIN
    
    UPDATE AspNetUsers
    SET PhoneNumber = @PhoneNumber,
        UserName = @FullName
    WHERE Id = @UserId

    UPDATE MedicalRecords
    SET TreatmentPlan = @TreatmentPlan,
        DiagnosisDetails = @DiagnosisDetails
    WHERE MedicalRecordNumber = @UserId

    IF @@ROWCOUNT = 0
    BEGIN
        RETURN 404;
    END

    RETURN 200;

END

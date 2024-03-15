DROP PROCEDURE IF EXISTS UpdateRecordsSelf;
GO
CREATE PROCEDURE UpdateRecordsSelf
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

END

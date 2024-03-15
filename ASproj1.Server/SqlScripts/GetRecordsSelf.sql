DROP PROCEDURE IF EXISTS GetRecordsSelf;
GO
CREATE PROCEDURE GetRecordsSelf
    @UserId NVARCHAR(MAX)
AS
BEGIN
    
    DECLARE @Result TABLE (
        UserName NVARCHAR(MAX),
        PhoneNumber NVARCHAR(MAX),
        DiagnosisDetails NVARCHAR(MAX),
        TreatmentPlan NVARCHAR(MAX)
    )

    INSERT INTO @Result (UserName, PhoneNumber, DiagnosisDetails, TreatmentPlan)
    SELECT UserName, PhoneNumber, DiagnosisDetails, TreatmentPlan
    FROM AspNetUsers 
    JOIN MedicalRecords ON AspNetUsers.Id = MedicalRecords.MedicalRecordNumber
    JOIN Patients ON AspNetUsers.Id = Patients.PatientId
    WHERE AspNetUsers.Id = @UserId;

    SELECT * FROM @Result;

END


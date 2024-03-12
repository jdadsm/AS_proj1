DROP PROCEDURE GetRecords;
GO
CREATE PROCEDURE GetRecords
    @UserId NVARCHAR(MAX)
AS
BEGIN
    
    DECLARE @Result TABLE (
        UserName NVARCHAR(MAX),
        PhoneNumber NVARCHAR(MAX),
        DiagnosisDetails NVARCHAR(MAX),
        TreatmentPlan NVARCHAR(MAX)
    )

    -- Insert the selected data into the table variable
    INSERT INTO @Result (UserName, PhoneNumber, DiagnosisDetails, TreatmentPlan)
    SELECT UserName, PhoneNumber, DiagnosisDetails, TreatmentPlan
    FROM AspNetUsers 
    JOIN MedicalRecords ON AspNetUsers.Id = MedicalRecords.MedicalRecordNumber
    WHERE AspNetUsers.Id = @UserId;

    -- Return the result set
    SELECT * FROM @Result;

END


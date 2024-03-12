CREATE USER SelectUser WITHOUT LOGIN;

GRANT SELECT TO MaskingTestUser;
  
-- impersonate for testing:
EXECUTE AS USER = 'MaskingTestUser';

REVERT;

SELECT * FROM Patients
SELECT * FROM AspNetUsers
SELECT * FROM MedicalRecords

SELECT UserName,PhoneNumber,DiagnosisDetails,TreatmentPlan
FROM AspNetUsers JOIN MedicalRecords ON Id = MedicalRecordNumber
WHERE Id = '129a54db-0574-41c7-9674-807a76005490'

EXEC GetRecords '129a54db-0574-41c7-9674-807a76005490'
    
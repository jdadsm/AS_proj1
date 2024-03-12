ALTER TABLE AspNetUsers
ALTER COLUMN PhoneNumber ADD MASKED WITH (FUNCTION = 'default()');
GO
ALTER TABLE MedicalRecords
ALTER COLUMN DiagnosisDetails ADD MASKED WITH (FUNCTION = 'default()');
GO
ALTER TABLE MedicalRecords
ALTER COLUMN TreatmentPlan ADD MASKED WITH (FUNCTION = 'default()');
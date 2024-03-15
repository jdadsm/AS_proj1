IF EXISTS (SELECT 1 FROM Patients WHERE PatientId = '1')
    DELETE FROM Patients WHERE PatientId = '1';

IF EXISTS (SELECT 1 FROM AspNetUsers WHERE Id = '1')
    DELETE FROM AspNetUsers WHERE Id = '1';

IF EXISTS (SELECT 1 FROM MedicalRecords WHERE MedicalRecordNumber = '1')
    DELETE FROM MedicalRecords WHERE MedicalRecordNumber = '1';
GO
INSERT INTO AspNetUsers (Id, UserName, Email, PasswordHash, PhoneNumber, AccessFailedCount, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled)
VALUES
('1', 'admin', 'admin@admin.com', 'AQAAAAIAAYagAAAAEKXZuXQ538Gmk7IAV45eBDovf0kNQpf+PIzdkpXnFxiBt7xrTm8SEHfNRr1nqYTXHg==', '123456789', 0, 0, 0, 0, 0);
GO
INSERT INTO MedicalRecords (MedicalRecordNumber, DiagnosisDetails, TreatmentPlan)
VALUES
('1', '', '');
GO
INSERT INTO Patients (PatientId, MedicalRecordId, UserId, AccessCode, Role)
VALUES
('1', '1', '1', '', 'HelpDesk');
GO
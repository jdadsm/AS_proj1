CREATE USER SelectUser WITHOUT LOGIN;

GRANT SELECT TO SelectUser;
USE master
-- impersonate for testing:
EXECUTE AS USER = 'HelpDesk';
EXECUTE AS USER = 'DefaultUser';

REVERT;

SELECT * FROM Patients
SELECT * FROM AspNetUsers
SELECT * FROM MedicalRecords

SELECT * FROM sys.database_principals

UPDATE Patients
SET Role = 'HelpDesk'
WHERE PatientId = '6acc8cbf-ca6c-4b20-afba-a665938e4f86'


EXEC GetRecords '129a54db-0574-41c7-9674-807a76005490'
    
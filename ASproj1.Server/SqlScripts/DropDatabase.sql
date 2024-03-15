USE master;
SELECT * FROM sys.dm_tran_session_transactions;
GO
ALTER DATABASE "ReactApp22.Server" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE "ReactApp22.Server"
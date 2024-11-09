-- Create the database
CREATE DATABASE [$(MSSQL_DATABASE)];
GO

-- Switch to the new database
USE [$(MSSQL_DATABASE)];
GO

-- Create the login
CREATE LOGIN [$(MSSQL_USER)] WITH PASSWORD = N'$(MSSQL_PASSWORD)', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO

-- Create the user for the login
CREATE USER [$(MSSQL_USER)] FOR LOGIN [$(MSSQL_USER)];
GO

-- Grant DDL access by adding to db_ddladmin role
ALTER ROLE db_ddladmin ADD MEMBER [$(MSSQL_USER)];
GO

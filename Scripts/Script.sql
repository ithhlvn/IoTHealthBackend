--Step 1: Create the database with specified file paths
USE master
CREATE DATABASE IOT
ON
PRIMARY (
    NAME = 'IOT', 
    FILENAME = 'D:\Databases\IOT.mdf',  -- Path to the data file
    SIZE = 10MB,  -- Initial size of the data file
    MAXSIZE = 100MB,  -- Maximum size of the data file
    FILEGROWTH = 5MB  -- Growth increment for the data file
)
LOG ON (
    NAME = 'IOT_Log', 
    FILENAME = 'D:\Databases\IOT_log.ldf',  -- Path to the log file
    SIZE = 5MB,  -- Initial size of the log file
    MAXSIZE = 50MB,  -- Maximum size of the log file
    FILEGROWTH = 2MB  -- Growth increment for the log file
);

USE IOT;  -- Switch to the 'IOT' database

--Step 2: Grant necessary permissions
GRANT CREATE TABLE TO iot;  -- Allow creating tables
GRANT ALTER TO iot;  -- Allow altering tables
GRANT CREATE PROCEDURE TO iot;  -- Allow creating procedures
GRANT CREATE VIEW TO iot;  -- Allow creating views
GRANT EXECUTE TO iot;  -- Allow executing stored procedures and functions

-- If you want to allow full control of the schema:
GRANT CONTROL ON SCHEMA::dbo TO iot;  -- Full schema control

ALTER ROLE db_owner ADD MEMBER iot;




CREATE DATABASE LOGGER;

USE LOGGER;

CREATE TABLE loggerTable(
  logID INT  PRIMARY KEY IDENTITY(1,1),
  logData NVARCHAR(MAX),
  createdDate NVARCHAR(MAX)
);

INSERT INTO loggerTable (logData,createdDate) VALUES ('test','test');

SELECT * FROM loggerTable;
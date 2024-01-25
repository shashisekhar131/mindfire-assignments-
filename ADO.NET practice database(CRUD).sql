
CREATE DATABASE StudentDB;

USE StudentDB;
GO

CREATE TABLE StudentTable(
 Id INT PRIMARY KEY,
 Name VARCHAR(100),
 Email VARCHAR(50),
 Mobile VARCHAR(50)
)
GO



INSERT INTO StudentTable VALUES (101, 'Anurag', 'Anurag@dotnettutorial.net', '1234567890')
INSERT INTO StudentTable VALUES (102, 'Priyanka', 'Priyanka@dotnettutorial.net', '2233445566')
INSERT INTO StudentTable VALUES (103, 'Preety', 'Preety@dotnettutorial.net', '6655443322')
INSERT INTO StudentTable VALUES (104, 'Sambit', 'Sambit@dotnettutorial.net', '9876543210')
GO

SELECT * FROM StudentTable;
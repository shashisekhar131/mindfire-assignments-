CREATE DATABASE SchoolDB;
GO

USE SchoolDB;
GO

CREATE TABLE Student(
 Id INT PRIMARY KEY,
 Name VARCHAR(100),
 Email VARCHAR(50),
 Mobile VARCHAR(50)
)
GO

INSERT INTO Student VALUES (101, 'Anurag', 'Anurag@dotnettutorial.net', '1234567890')
INSERT INTO Student VALUES (102, 'Priyanka', 'Priyanka@dotnettutorial.net', '2233445566')
INSERT INTO Student VALUES (103, 'Preety', 'Preety@dotnettutorial.net', '6655443322')
INSERT INTO Student VALUES (104, 'Sambit', 'Sambit@dotnettutorial.net', '9876543210')
GO

SELECT * FROM Student;
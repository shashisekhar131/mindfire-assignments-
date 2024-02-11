

CREATE DATABASE UserManagement;


USE UserManagement;

CREATE TABLE UserDetails(
 UserID INT PRIMARY KEY IDENTITY(1,1),
 FirstName NVARCHAR(max),
 LastName NVARCHAR(max),
 Password NVARCHAR(max),
 PhoneNumber NVARCHAR(max),
 AlternatePhoneNumber NVARCHAR(max),
 Email NVARCHAR(max),
 AlternateEmail NVARCHAR(max),
 DOB nvarchar(max),
 Favouritecolor NVARCHAR(max),
 Aadhaar NVARCHAR(max),
 PAN NVARCHAR(max),
 PreferedLanguage nvarchar(max),
 MaritalStatus nvarchar(max),
 Upto10th nvarchar(max),
 PercentageUpto10th INT,
 Upto12th nvarchar(max),
 PercentageUpto12th INT,
 Graduation nvarchar(max),
 PercentageInGraduation INT
);

CREATE TABLE AddressDetails(
  ID INT PRIMARY KEY IDENTITY(1,1),
  Address NVARCHAR(max),
  Type NVARCHAR(max),
  UserID INT FOREIGN KEY REFERENCES UserDetails(UserID)
);


CREATE TABLE Notes(
NotesID  INT PRIMARY KEY IDENTITY(1,1),
NoteText nvarchar(max), 
Page nvarchar(max),
CreatedDate nvarchar(max),
 UserID INT FOREIGN KEY REFERENCES UserDetails(UserID)
);

SELECT * FROM UserDetails;
SELECT * FROM AddressDetails;
SELECT * FROM Notes;

INSERT INTO UserDetails (firstname) VALUES ('hello');

SELECT * FROM AddressDetails WHERE UserID = 1;

DELETE FROM AddressDetails;

DELETE FROM UserDetails;
-- Reseed UserDetails table
DBCC CHECKIDENT ('UserDetails', RESEED, 0);

-- Reseed AddressDetails table
DBCC CHECKIDENT ('AddressDetails', RESEED, 0);


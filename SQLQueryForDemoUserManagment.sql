

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
  Address NVARCHAR(max) NOT NULL,
  Type INT NOT NULL,
  UserID INT FOREIGN KEY REFERENCES UserDetails(UserID) NOT NULL,
   CountryID INT FOREIGN KEY REFERENCES Country(CountryID) NOT NULL,
   StateID INT FOREIGN KEY REFERENCES State(StateID) NOT NULL
);

CREATE TABLE Country(
 CountryID INT PRIMARY KEY IDENTITY(1,1),
 CountryName NVARCHAR(max) NOT NULL,
 );

 CREATE TABLE State(
 StateID INT PRIMARY KEY IDENTITY(1,1),
 StateName NVARCHAR(max) NOT NULL,
 CountryID INT FOREIGN KEY REFERENCES Country(CountryID) NOT NULL
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
SELECT * FROM Country;
SELECT * FROM State;
INSERT INTO UserDetails (firstname) VALUES ('hello');

SELECT * FROM AddressDetails WHERE UserID = 1;

DELETE FROM AddressDetails;
DELETE  FROM Notes;
DELETE FROM UserDetails;


INSERT INTO Country valueS ('India'),('US');


INSERT INTO State values ('delhi',1),('mumbai',1),('texas',2),('california',2);


DELETE  FROM State;
DELETE FROM Country;
-- Reseed UserDetails table
DBCC CHECKIDENT ('UserDetails', RESEED, 0);

-- Reseed AddressDetails table
DBCC CHECKIDENT ('AddressDetails', RESEED, 0);

--Reseed Notes table 
DBCC CHECKIDENT ('Notes', RESEED, 0);

DBCC CHECKIDENT ('State', RESEED, 0);
DBCC CHECKIDENT ('Country', RESEED, 0);


DROP TABLE AddressDetails;
DROP TABLE Country;
DROP TABLE State;


CREATE TABLE Document(
 DocumentID INT PRIMARY KEY IDENTITY(1,1),
 ObjectType INT NOT NULL,
 DocumentOriginalName nvarchar(max),
 DocumentGuidName nvarchar(max),
 ObjectID INT NOT NULL
);

SELECT * FROM Document;
ALTER TABLE Document
ADD TimeStamp nvarchar(max);


ALTER TABLE Document
ADD Documen Type INT NOT NULL;

EXEC sp_rename 'Document.DocumenType', 'DocumentType', 'COLUMN';


DROP TABLE Document;


CREATE TABLE UserRole(
UserRoleID INT PRIMARY KEY IDENTITY(1,1),
UserID INT NOT NULL,
RoleID INT NOT NULL
);

-- look up table Role it contains what are all the roles
CREATE TABLE Role(
RoleID INT PRIMARY KEY IDENTITY(1,1),
RoleName nvarchar(max),
IsDefault INT NOT NULL,
IsAdmin INT NOT NULL
);

INSERT INTO Role VALUES ('admin',0,1);
INSERT INTO Role VALUES ('standard user',1,0);




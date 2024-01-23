
CREATE DATABASE db;
USE db;

CREATE TABLE Habitat(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(64)
);


INSERT INTO Habitat VALUES
('Forest'),
('River'),
('urban areas');

CREATE TABLE Animal(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(64),
Species VARCHAR(64),
HabitatId INT, 
FOREIGN KEY (HabitatId)
REFERENCES Habitat(Id)
);

INSERT INTO Animal VALUES 
('roary','Lion',1),
('barky','dog',3),
('swimy','fish',2)
;

INSERT INTO Animal VALUES 
('brother-roary','Lion',1),
('brother-barky','dog',3),
('brother-swimy','fish',2)
;

INSERT INTO Animal VALUES ('sister-swimy','fish',2);

SELECT * FROM Animal;

-- names of all habitats in animal 
SELECT Name FROM Habitat;

-- select animal species who live in urban areas
SELECT * FROM Animal INNER JOIN Habitat ON Animal.HabitatId = Habitat.Id WHERE Habitat.Name = 'urban areas';

-- select count of each animal species 
SELECT Species, COUNT(Species) AS "number of animals" FROM Animal GROUP BY Species;

-- total no.of animals in each habitat 

SELECT Habitat.Name,COUNT(Animal.Name) FROM Animal INNER JOIN Habitat ON Animal.HabitatId = Habitat.Id GROUP BY Habitat.Name;

-- habitats with no.of animals >2 
SELECT Habitat.Name FROM Animal INNER JOIN Habitat ON Animal.HabitatId = Habitat.Id GROUP BY Habitat.Name HAVING COUNT(Habitat.Name)>2;

-- animals with their species > 2 

SELECT Species FROM Animal GROUP BY Species HAVING COUNT(Species) >2;

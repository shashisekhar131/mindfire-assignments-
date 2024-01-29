

CREATE DATABASE XYZSchoolDB;

USE XYZSchoolDB;



CREATE TABLE Student (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX)
);

INSERT INTO Student (Name) VALUES ('John Doe'),('Jane Smith');

CREATE TABLE Grade (
    GradeID INT PRIMARY KEY IDENTITY(1,1),
    GradeName NVARCHAR(MAX)
);
INSERT INTO Grade (GradeName) VALUES ('8th grade'),('9th grade');

CREATE TABLE Course (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(MAX)
);
INSERT INTO Course (CourseName) VALUES ('Mathematics'), ('Literature');

CREATE TABLE StudentGrade (
    StudentID INT,
    GradeID INT,
    PRIMARY KEY (StudentID, GradeID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (GradeID) REFERENCES Grade(GradeID)
);
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (1, 1); -- John Doe in 8th Grade
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (2, 2); -- Jane Smith in 9th Grade

CREATE TABLE CourseGrade (
    CourseID INT,
    GradeID INT,
    PRIMARY KEY (CourseID, GradeID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    FOREIGN KEY (GradeID) REFERENCES Grade(GradeID)
);

INSERT INTO CourseGrade (CourseID, GradeID) VALUES (1, 1); -- Mathematics in 8th Grade
INSERT INTO CourseGrade (CourseID, GradeID) VALUES (2, 2); -- Literature in 9th Grade

SELECT * FROM StudentGrade;
SELECT * FROM CourseGrade;
SELECT * FROM Course;


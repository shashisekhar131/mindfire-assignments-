

CREATE DATABASE XYZSchoolDB;

USE XYZSchoolDB;



CREATE TABLE Student (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(MAX)
);

INSERT INTO Student (Name) VALUES ('John Doe'),('Jane Smith'),('ram'),('teja');
INSERT INTO Student (Name) VALUES ('virat'),('rohit'),('rahul');

CREATE TABLE Grade (
    GradeID INT PRIMARY KEY IDENTITY(1,1),
    GradeName NVARCHAR(MAX)
);
INSERT INTO Grade (GradeName) VALUES ('8th grade'),('9th grade'),('10th grade'),('11th grade');

CREATE TABLE Course (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(MAX)
);
INSERT INTO Course (CourseName) VALUES ('Basic Mathematics'), ('Literature'),('Advanced Mathematics'),('physics'),('drawing');

CREATE TABLE StudentGrade (
    StudentID INT,
    GradeID INT,
    PRIMARY KEY (StudentID, GradeID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (GradeID) REFERENCES Grade(GradeID)
);
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (6, 5); -- John Doe in 8th Grade
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (7, 5); -- Jane Smith in 9th Grade
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (11, 6); -- virat in 9th Grade
INSERT INTO StudentGrade (StudentID, GradeID) VALUES (9, 8); -- ram in 11th Grade

CREATE TABLE CourseGrade (
    CourseID INT,
    GradeID INT,
    PRIMARY KEY (CourseID, GradeID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    FOREIGN KEY (GradeID) REFERENCES Grade(GradeID)
);

INSERT INTO CourseGrade (CourseID, GradeID) VALUES (7, 5); -- basic Mathematics in 8th Grade
INSERT INTO CourseGrade (CourseID, GradeID) VALUES (8, 6); -- Literature in 9th Grade
INSERT INTO CourseGrade (CourseID, GradeID) VALUES (9, 8); -- advanced Mathematics in 11th Grade
INSERT INTO CourseGrade (CourseID, GradeID) VALUES (11, 5); -- drawing in 8th Grade


-- Drop foreign key constraints in referencing tables
ALTER TABLE StudentGrade
DROP CONSTRAINT IF EXISTS FK_StudentGrade_StudentID;

-- Recreate the foreign key constraint with ON DELETE CASCADE
ALTER TABLE StudentGrade
ADD CONSTRAINT FK_StudentGrade_StudentID
FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
ON DELETE CASCADE;

SELECT * FROM Student;
SELECT * FROM Grade;
SELECT * FROM Course;

SELECT * FROM StudentGrade;
SELECT * FROM CourseGrade;


--joins


SELECT StudentGrade.GradeID,Student.Name AS temp FROM StudentGrade INNER JOIN Student ON Student.StudentID = StudentGrade.StudentID;

-- first join StudentGrade, Student  and then join Grade with result table so that we can get GradeName from result table 
SELECT StudentGrade.StudentID, Student.Name, Grade.GradeName
FROM StudentGrade
INNER JOIN Student ON Student.StudentID = StudentGrade.StudentID
INNER JOIN Grade ON Grade.GradeID = StudentGrade.GradeID;


-- display no.of students in each grade 

SELECT (SELECT GradeName FROM Grade WHERE GradeId = StudentGrade.GradeID) AS grade_name,COUNT(Student.StudentID) AS no_of_students 
FROM StudentGrade
INNER JOIN Student ON Student.StudentID = StudentGrade.StudentID 
GROUP BY (GradeID);

-- list of students who are not assinged to any grade 

SELECT Name FROM Student LEFT JOIN StudentGrade ON Student.StudentID = StudentGrade.StudentID WHERE StudentGrade.StudentID IS NULL;

-- list of courses that are not assigned to any grade

SELECT Course.CourseName
FROM Course
LEFT JOIN CourseGrade ON Course.CourseID = CourseGrade.CourseID
WHERE CourseGrade.GradeID IS NULL;

-- grade with highest number of enrollments 

SELECT (SELECT GradeName FROM Grade WHERE GradeId = StudentGrade.GradeID) AS grade_name,COUNT(Student.StudentID) AS no_of_students 
FROM StudentGrade
INNER JOIN Student ON Student.StudentID = StudentGrade.StudentID 
GROUP BY (GradeID) ORDER BY COUNT(Student.StudentID) DESC;




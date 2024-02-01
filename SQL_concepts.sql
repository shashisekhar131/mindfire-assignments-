

use db;

CREATE TABLE employee(
 id INT PRIMARY KEY IDENTITY(1,1),
 name NVARCHAR(MAX),
 sal INT
 );

 INSERT INTO employee (name,sal) VALUES ('ram',20000),('bheem',55000),('akash',35000);
SELECT * FROM Animal;

-- local variables in SQL 
declare @x NVARCHAR(MAX);
SET @x  = (SELECT name FROM Animal WHERE id = 1);
PRINT @x;

-- looping in SQL 

declare @i INT;
SET @i = 1;

WHILE @i < = 5
BEGIN
PRINT @i;
SET @i = @i + 1;
END


-- dynamic query ( query built at runtime) 

DECLARE @col_name NVARCHAR(50) = 'Name';
DECLARE @search_term NVARCHAR(50) = 'Lion';

DECLARE @sql_query NVARCHAR(MAX) = 'SELECT * FROM Animal WHERE ' + @col_name + ' = '' ' + @search_term + '''';

PRINT @sql_query;

EXEC sp_executesql @sql_query


-- temporary table (stores intermediate result table in a session) 

CREATE TABLE #temp_table(
ID INT,
country NVARCHAR(MAX)
);

INSERT INTO #temp_table (ID,country) VALUES (1,'india'),(2,'usa');

SELECT * FROM #temp_table;



-- table variable (stores intermediate result table in a memory) 

declare @table_variable TABLE(
   ID INT,
   Name NVARCHAR(MAX)
 );

 INSERT INTO @table_variable (ID,Name) VALUES (1,'virat'),(2,'rahane');
 SELECT * FROM @table_variable;


-- CTE (comman table expression) 

-- for example i want to perform multiple queries on employees whose salary > 50000 then create cte for that query and use it 

WITH neededEmployees AS (
    SELECT * FROM employee WHERE sal > 50000
)
-- Now you can use the CTE in a subsequent query
SELECT name FROM neededEmployees;



-- cursor 


declare employeeCursor cursor for  
SELECT id,name FROM employee WHERE sal > 20000;


declare @id INT , @name NVARCHAR(max);

OPEN employeeCursor;

FETCH NEXT FROM employeeCursor INTO @id,@name;

WHILE @@FETCH_STATUS = 0 
BEGIN 

 PRINT @id;
 PRINT @name;

FETCH NEXT FROM employeeCursor INTO @id,@name;
END 

CLOSE employeeCursor;










-- stored procedure 



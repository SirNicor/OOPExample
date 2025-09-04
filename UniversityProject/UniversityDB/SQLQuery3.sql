USE UniversityDB

SELECT * FROM Address
SELECT _levelDegrees FROM DegreesStudy
SELECT _levelID AS LevelMillitary FROM IdMillitary 

SELECT _levelDegrees AS LevelDegrees, _levelID as LevelID 
INTO ENUMS
FROM DegreesStudy, IdMillitary
GO
	
SELECT * FROM ENUMS
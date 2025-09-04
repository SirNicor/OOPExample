USE UniversityDB
SELECT ID FROM Passport
WHERE _birthDate < '2005-01-01'
SELECT * FROM DegreesStudy 
SELECT * FROM IdMillitary
SELECT * FROM Student
GO

INSERT INTO Student
VALUES
(52, 1, 0, 1, NULL, null, NULL, NULL),
(53, 1, 0, 1, NULL, null, NULL, NULL),
(54, 1, 0, 1, NULL, null, NULL, NULL),
(55, 1, 0, 1, NULL, null, NULL, NULL),
(56, 1, 0, 1, NULL, null, NULL, NULL),
(57, 1, 0, 1, NULL, null, NULL, NULL),
(58, 1, 0, 1, NULL, null, NULL, NULL);
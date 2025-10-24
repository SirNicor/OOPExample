use UniversityDB
SELECT Student.ID as StudentID, _skipHours, _countOfExamsPassed, _creditScores, _accomodationDormitories,
Passport._serial, Passport._number, Passport._firstName, Passport._lastName,
Passport._middleName, Passport._birthDate, Address._country, Address._city, 
Address._street, Address._houseNumber, DegreesStudy._levelDegrees,
IdMillitary._levelID
FROM Student
	INNER JOIN Passport ON Student._passportID = Passport.ID
	INNER JOIN Address 
	ON Address.ID = Passport._addressID AND Student._passportID = Passport.ID
	INNER JOIN DegreesStudy ON Student._courseID = DegreesStudy.ID
	INNER JOIN IdMillitary ON STUDENT._millitaryID = IdMillitary.ID

SELECT Student.ID as StudentID, _skipHours, _countOfExamsPassed, _creditScores, _accomodationDormitories,
Passport._serial, Passport._number, Passport._firstName, Passport._lastName,
Passport._middleName, Passport._birthDate, Address._country, Address._city, 
Address._street, Address._houseNumber, DegreesStudy._levelDegrees,
IdMillitary._levelID
FROM Student
	RIGHT OUTER JOIN Passport ON Student._passportID = Passport.ID
	RIGHT OUTER JOIN Address 
	ON Address.ID = Passport._addressID AND Student._passportID = Passport.ID
	RIGHT OUTER JOIN DegreesStudy ON Student._courseID = DegreesStudy.ID
	RIGHT OUTER JOIN IdMillitary ON STUDENT._millitaryID = IdMillitary.ID

SELECT _skipHours, _countOfExamsPassed, _creditScores,
Passport._serial, Passport._number, Passport._firstName, Passport._lastName,
Passport._middleName, Passport._birthDate, Address._country, Address._city, 
Address._street, Address._houseNumber, DegreesStudy._levelDegrees,
IdMillitary._levelID
FROM Student
	INNER JOIN Passport ON Student._passportID = Passport.ID
	INNER JOIN Address 
	ON Address.ID = Passport._addressID AND Student._passportID = Passport.ID
	INNER JOIN DegreesStudy ON Student._courseID = DegreesStudy.ID
	INNER JOIN IdMillitary ON STUDENT._millitaryID = IdMillitary.ID

SELECT _skipHours, _countOfExamsPassed, _creditScores,
Passport._serial, Passport._number, Passport._firstName, Passport._lastName,
Passport._middleName, Passport._birthDate, Address._country, Address._city, 
Address._street, Address._houseNumber, DegreesStudy._levelDegrees,
IdMillitary._levelID
FROM Student
	INNER JOIN Passport ON Student._passportID = Passport.ID
	INNER JOIN Address 
	ON Address.ID = Passport._addressID AND Student._passportID = Passport.ID
	INNER JOIN DegreesStudy ON Student._courseID = DegreesStudy.ID
	INNER JOIN IdMillitary ON STUDENT._millitaryID = IdMillitary.ID

SELECT 
    s._skipHours AS SkipHours,
    s._countOfExamsPassed AS CountOfExamsPassed, 
    s._creditScores AS CreditScores,
    p._serial AS Serial,
    p._number AS Number,
    p._firstName AS FirstName,
    p._lastName AS LastName,
    p._middleName AS MiddleName,
    p._birthDate AS BirthDate,
    a._country AS Country,
    a._city AS City,
    a._street AS Street,
    a._houseNumber AS HouseNumber,
    ds._levelDegrees AS LevelDegrees,
    im._levelID AS LevelID
FROM Student s
INNER JOIN Passport p ON s._passportID = p.ID
INNER JOIN Address a ON p._addressID = a.ID
INNER JOIN DegreesStudy ds ON s._courseID = ds.ID
INNER JOIN IdMillitary im ON s._millitaryID = im.ID
WHERE S.ID = 1;	
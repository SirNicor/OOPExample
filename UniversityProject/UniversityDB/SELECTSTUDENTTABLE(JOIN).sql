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
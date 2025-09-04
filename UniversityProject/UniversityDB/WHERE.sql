USE UniversityDB

SELECT * FROM Address
WHERE _city = 'St.Petersburg'

SELECT * FROM Address
WHERE (_city = 'St.Petersburg' AND _street = 'Nevsky prospect' 
OR _city = 'Moscow' AND _street = 'TsvetnoyBlvd') AND NOT(_houseNumber = 14)


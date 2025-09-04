USE UniversityDB

SELECT COUNT(*) FROM Passport 
SELECT COUNT(*) FROM Passport GROUP BY _addressID
SELECT _city, _street, COUNT(*) FROM Address GROUP BY _city, _street
SELECT _city, _street, COUNT(*) 
FROM Address GROUP BY _city, _street
HAVING COUNT(*) < 2
SELECT _city, _street, COUNT(*) 
FROM Address 
WHERE _city <> 'rostov-on-don'
GROUP BY _city, _street
HAVING COUNT(*) < 2
SELECT _city, _street, COUNT(*)
FROM Address 
GROUP BY _city, _street 
WITH ROLLUP
SELECT _city, _street, COUNT(*)
FROM Address 
GROUP BY _city, _street 
WITH CUBE
SELECT _city, _street, COUNT(*)
FROM Address 
GROUP BY GROUPING SETS(_city, _street)
SELECT _city, _street, 
COUNT(*) OVER (PARTITION BY _city)
FROM Address 
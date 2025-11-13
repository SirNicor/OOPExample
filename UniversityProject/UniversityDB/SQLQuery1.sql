SELECT ad.Id as AdministratorId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM PersonalOfUniversity PU
JOIN Administrator ad ON ad.Id = PU.IdAdministrator
INNER JOIN Passport p ON ad.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID
WHERE IdUniversity = 1;
SELECT un.Id, un.NameUniversity, un.Budget, ad.Id as AdministratorId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM University un
JOIN Administrator ad ON ad.Id = un.Rector
INNER JOIN Passport p ON ad.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID
WHERE UN.ID = 1;
SELECT un.Id, un.NameUniversity, un.Budget, ad.Id as AdministratorId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber, ad.Id as AdministratorId, ad.Salary, ad.CriminalRecord,
ad.MilitaryID, ad.PassportID, p.Serial, p.Number, p.FirstName, p.LastName,
p.MiddleName, p.BirthData, p.AddressId, a.Country, a.City, a.Street, a.HouseNumber FROM PersonalOfUniversity PU
JOIN University un ON un.Id = PU.IdUniversity
JOIN Administrator rec ON rec.Id = un.Rector
INNER JOIN Passport rp ON rec.PassportId = rp.ID
INNER JOIN Address ra ON rp.AddressId = ra.ID
INNER JOIN IdMilitary rim ON rec.MilitaryId = rim.ID
JOIN Administrator ad ON ad.Id = PU.IdAdministrator
INNER JOIN Passport p ON ad.PassportId = p.ID
INNER JOIN Address a ON p.AddressId = a.ID
INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID
WHERE IdUniversity = 1;
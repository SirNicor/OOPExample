namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class WorkerAdministratorRepository : IWorkerAdministratorRepository
{
    private const string SqlQuerySelect = @"
    SELECT 
        ad.Salary AS Salary,
        ad.CriminalRecord as CriminalRecord,
        im.LevelId AS MilitaryIdAvailability,
        p.ID as PassportID,
        p.Serial AS Serial,
        p.Number AS Number,
        p.FirstName AS FirstName,
        p.LastName AS LastName,
        p.MiddleName AS MiddleName,
        p.BirthData AS BirthDate,
        a.ID as AddressID,
        a.Country AS Country,
        a.City AS City,
        a.Street AS Street,
        a.HouseNumber AS HouseNumber
    FROM Administrator ad
    INNER JOIN Passport p ON ad.PassportId = p.ID
    INNER JOIN Address a ON p.AddressId = a.ID
    INNER JOIN IdMilitary im ON ad.MilitaryId = im.ID";
    public WorkerAdministratorRepository(IGetConnectionString getConnectionString, MyLogger logger)
    {
        ConnectionString = getConnectionString.ReturnConnectionString();
        _myLogger = logger;
    }
    public void PrintAll()
    {
        using(IDbConnection db = new SqlConnection(ConnectionString))
        {
            List<Administrator> Administrators = db.Query<Administrator, Passport, Address, Administrator>(SqlQuerySelect,
                (Administrator, Passport, Address) =>
                {
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                }, 
                splitOn: "PassportId, AddressId").ToList();
            foreach (Administrator admin in Administrators)
            {
                admin.PrintDerivedClass(_myLogger);
            }
        }
    }
    public List<Administrator> ReturnListAdministrator()
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.Query<Administrator, Passport, Address, Administrator>(
                SqlQuerySelect,
                (Administrator, Passport, Address) =>
                {
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                },
                splitOn: "PassportId, AddressId").ToList();
        }
    }

    public Administrator Get(int ID)
    {
        Administrator administrator = null;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            administrator = db.Query<Administrator, Passport, Address, Administrator>(
                SqlQuerySelect + " WHERE ad.ID = @ID",
                (Administrator, Passport, Address) =>
                {
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                },
                splitOn: "PassportId, AddressId").FirstOrDefault();
        }
        _myLogger.Info("Return administrator - " + administrator.Passport.Serial + administrator.Passport.Number);
        return administrator;
    }
    
    public int Create(Administrator worker)
    {
        var salary = worker.Salary;
        var criminalRecord = worker.CriminalRecord;
        var millitaryIdAvailability = worker.MilitaryIdAvailability;
        var passport = worker.Passport;
        var address = passport.Address;
        var city =  address.City;
        var houseNumber = address.HouseNumber;
        var country = address.Country;
        var street = address.Street;
        var serial = passport.Serial;
        var number = passport.Number;
        var firstName = passport.FirstName;
        var lastName = passport.LastName;
        var middleName = passport.MiddleName;
        var birthData =  passport.BirthData;
        var placeReceipt = passport.PlaceReceipt;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        _myLogger.Info("Start Transaction");
                        var sqlQuery = @"
                INSERT INTO Address 
                VALUES(@country, @city, @street, @houseNumber)
                INSERT INTO Passport
                       VALUES(@serial,
                           @number,
                           @firstName,
                           @lastName, 
                           @middleName, 
                           @birthData, 
                           (SELECT MAX(ID) FROM ADDRESS), 
                           @placeReceipt)
                INSERT INTO Administrator
                    VALUES(@salary,
                        @criminalRecord,
                        (SELECT MAX(ID) FROM PASSPORT),
                        @millitaryIdAvailability + 1
                        )";
                        db.Execute(sqlQuery, new
                        {
                            country, city, street, houseNumber, serial, number,firstName, lastName, middleName, birthData,placeReceipt, salary,
                            millitaryIdAvailability, criminalRecord
                        }, transaction);
                        transaction.Commit();
                        _myLogger.Info("End Transaction");
                        var id = db.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM Administrator");
                        return id;
                    }
                    catch(Exception ex)
                    {
                        _myLogger.Error("An error occured during transaction" + ex.Message);
                        transaction.Rollback();
                        return -1;
                    }
                }
        }
    }

    public void Delete(int ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Execute("DELETE FROM Address WHERE ID = @ID", new {ID});
            _myLogger.Info("Delete administrator - " + ID);
        }
    }
    
    string ConnectionString = null;
    private MyLogger _myLogger;
}
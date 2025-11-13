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
        ad.Id AS PersonId,
        ad.Salary,
        ad.CriminalRecord,
        im.LevelId AS MilitaryIdAvailability,
        p.ID AS PassportID,
        p.Serial,
        p.Number,
        p.FirstName,
        p.LastName,
        p.MiddleName,
        p.BirthData,
        a.ID AS AddressID,
        a.Country,
        a.City,
        a.Street,
        a.HouseNumber
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
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var administrator = db.Query<Administrator, Passport, Address, Administrator>(
                SqlQuerySelect + " WHERE ad.ID = @ID",
                (admin, Passport, Address) =>
                {
                    Passport.Address = Address;
                    admin.Passport = Passport;
                    return admin;
                }, new{ID},
                splitOn: "PassportId, AddressId").FirstOrDefault();
            _myLogger.Info($"Return administrator - {administrator?.Passport?.Serial ?? -1}, Number: {administrator?.Passport?.Number ?? -1}");
            return administrator;
        }
    }   
    
    public int Create(Administrator worker)
    {
        var passport = worker.Passport;
        var address = passport.Address;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using(IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        _myLogger.Info("Start Transaction");
                        var sqlQuery = @"
                INSERT INTO Address(Country, City, Street, HouseNumber)
                VALUES(@Country, @City, @Street, @HouseNumber)";
                        db.Execute(sqlQuery, address, transaction);
                        sqlQuery = @"
                INSERT INTO Passport(Serial, Number, FirstName, LastName, MiddleName, BirthData, AddressId, PlaceReceipt)
                       VALUES(@Serial,
                           @Number,
                           @FirstName,
                           @LastName, 
                           @MiddleName, 
                           @BirthData, 
                           (SELECT MAX(ID) FROM ADDRESS), 
                           @PlaceReceipt)";
                        db.Execute(sqlQuery, passport, transaction);
                        sqlQuery = @"
                INSERT INTO Administrator(Salary, CriminalRecord, PassportId, MilitaryId)
                    VALUES(@Salary,
                        @CriminalRecord,
                        (SELECT MAX(ID) FROM PASSPORT),
                        @MilitaryIdAvailability + 1)";
                        db.Execute(sqlQuery, worker, transaction);
                        transaction.Commit();
                        _myLogger.Info("End Transaction");
                        var id = db.QueryFirstOrDefault<int>("SELECT MAX(ID) FROM Administrator");
                        return id;
                    }
                    catch(Exception ex)
                    {
                        _myLogger.Error("An error occured during transaction" + ex.Message);
                        transaction.Rollback();
                        throw;
                    }
                }
        }
    }

    public int Update(Administrator administrator)
    {
        var passport = administrator.Passport;
        var address = passport.Address;
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string sqlQuery = @"SELECT PassportID FROM Administrator WHERE Id = @PersonID";
                    passport.PassportId = db.Query<int>(sqlQuery, administrator, transaction).First();
                    sqlQuery = @"SELECT AddressId FROM Passport WHERE Id = @PassportID";
                    address.AddressId = db.Query<int>(sqlQuery, passport, transaction).First();
                    sqlQuery = @"UPDATE Address 
                SET Country = @Country,  City = @City, Street = @Street, HouseNumber = @HouseNumber
                WHERE ID = @AddressId";
                    db.Execute(sqlQuery, address , transaction);
                    sqlQuery = @"
                    UPDATE PASSPORT 
                    SET Serial = @Serial, 
                        Number = @Number,  
                        FirstName = @FirstName, 
                        LastName = @LastName, 
                        MiddleName = @MiddleName, 
                        BirthData = @BirthData, 
                        PlaceReceipt = @PlaceReceipt
                    WHERE ID = @PassportID";
                    db.Execute(sqlQuery, passport, transaction);
                    sqlQuery = @"UPDATE Administrator
                    SET Salary = @Salary, MilitaryId = @MilitaryIdAvailability, CriminalRecord = @CriminalRecord
                    WHERE ID = @PersonId";
                    db.Execute(sqlQuery, administrator, transaction);
                    transaction.Commit();
                    return administrator.PersonId;
                }
                catch(Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    throw;
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
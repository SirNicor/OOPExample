namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using IRepositoryAll;
public class WorkerAdministratorRepository : IWorkerAdministratorRepository
{
    private const string SqlQuerySelect = @"
    SELECT 
        ad.Id AS PersonId,
        ad.Salary,
        ad.CriminalRecord,
        im.Id AS MillitaryId,
        im.LevelId AS LevelId,
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
            List<Administrator> Administrators = db.Query<Administrator, MillitaryClass, Passport, Address, Administrator>(SqlQuerySelect,
                (Administrator, millitary, Passport, Address) =>
                {
                    Administrator.Millitary = millitary;
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                }, 
                splitOn: "MillitaryId, PassportId, AddressId").ToList();
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
            return db.Query<Administrator, MillitaryClass, Passport, Address, Administrator>(
                SqlQuerySelect,
                (Administrator, millitary, Passport, Address) =>
                {
                    Administrator.Millitary = millitary;
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                }, 
                splitOn: "MillitaryId, PassportId, AddressId").ToList();
        }
    }

    public Administrator Get(long ID)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            var administrator = db.Query<Administrator, MillitaryClass, Passport, Address, Administrator>(
                SqlQuerySelect + " WHERE ad.ID = @ID",
                (Administrator, millitary, Passport, Address) =>
                {
                    Administrator.Millitary = millitary;
                    Passport.Address = Address;
                    Administrator.Passport = Passport;
                    return Administrator;
                }, new{ID},
                splitOn: "MillitaryId, PassportId, AddressId").FirstOrDefault();
            _myLogger.Info($"Return administrator - {administrator.Passport.Serial}, Number: {administrator.Passport.Number}");
            return administrator;
        }
    }   
    
    public long Create(Administrator worker)
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
                INSERT INTO Administrator(Salary, CriminalRecord, PassportId, MilitaryId, Post)
                    VALUES(@Salary,
                        @CriminalRecord,
                        (SELECT MAX(ID) FROM PASSPORT),
                        @MillitaryId,
                        @Post";
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

    public long Update(Administrator administrator)
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
                    SET Salary = @Salary, MilitaryId = @MillitaryId, CriminalRecord = @CriminalRecord
                    WHERE ID = @PersonId";
                    db.Execute(sqlQuery, administrator, transaction);
                    transaction.Commit();
                    return administrator.AdministratorId;
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
    public void Delete(long Id)
    {
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            db.Execute("DELETE FROM Administrator WHERE ID = @ID", new { ID = Id});
            _myLogger.Info("Delete administrator - " + Id);
        }
    }
    
    string ConnectionString = null;
    private MyLogger _myLogger;
}
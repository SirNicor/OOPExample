namespace Repository;
using UCore;
using Logger;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class UniversityRepository : IUniversityRepository
{
    private const string SqlSelectIdUniversityQuery = @"Select 
    un.Id AS ID
    FROM University un";
    private const string SqlSelectNameUniversityQuery = @"Select 
    un.NameUniversity 
    FROM University un";

    private const string SqlSelectBudgetUniversityQuery = @"Select 
    un.Budget AS BudgetSize
    FROM University un";

    private const string SqlSelectRectorUniversityQuery = @"Select 
    un.Rector
    FROM University un";
    private const string SqsSelectPersonalOfAdministratorQuery = @"SELECT 
    IdUniversity,
    IdAdministrator
FROM PersonalOfUniversity";
    private const string SqsSelectPersonalOfAdministratorQuery1 = @"
SELECT 
    IdAdministrator
FROM PersonalOfUniversity WHERE IdUniversity = @ID";
    public UniversityRepository(IGetConnectionString getConnectionString, MyLogger logger, IWorkerAdministratorRepository workerAdministratorRepository)
    {
        _connectionString =  getConnectionString.ReturnConnectionString();
        _myLogger = logger;
        _workerAdministratorRepository = workerAdministratorRepository;
    }

    public University Get(int ID)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            List<Administrator> administrators = new List<Administrator>();
            var idadministrator = db.Query<int>(SqsSelectPersonalOfAdministratorQuery1, new { ID }).ToList();
            foreach (int id in idadministrator)
            {
                administrators.Add(_workerAdministratorRepository.Get(id));
            }
            string nameUniversity = db.Query<string>(SqlSelectNameUniversityQuery + " Where ID = @ID", new { ID }).FirstOrDefault();
            int  budgetSize = db.Query<int>(SqlSelectBudgetUniversityQuery + " Where ID = @ID", new { ID }).FirstOrDefault();
            int idRector = db.Query<int>(SqlSelectRectorUniversityQuery + " Where ID = @ID", new { ID }).FirstOrDefault();
            University university = new University();
            university.NameUniversity = nameUniversity;
            university.BudgetSize = budgetSize;
            university.Rector = _workerAdministratorRepository.Get(idRector);
            university.Administrators = administrators;
            return university;
        }
    }

    public List<University> ReturnList()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            int count = db.Query(SqlSelectIdUniversityQuery).ToList().Count;
            List<University> universities = new List<University>();
            for (int idUniversity = 1; idUniversity < count+1; idUniversity++)
            {
                List<Administrator> administrators = new List<Administrator>();
                var Idadministrator = db.Query<int>(SqsSelectPersonalOfAdministratorQuery1, new { ID = idUniversity }).ToList();
                foreach (int i in Idadministrator)
                {
                    administrators.Add(_workerAdministratorRepository.Get(i));
                }
                string nameUniversity = db.Query<string>(SqlSelectNameUniversityQuery + " Where ID = @ID", new { ID = idUniversity }).FirstOrDefault();
                int  budgetSize = db.Query<int>(SqlSelectBudgetUniversityQuery + " Where ID = @ID", new { ID = idUniversity }).FirstOrDefault();
                int idRector = db.Query<int>(SqlSelectRectorUniversityQuery + " Where ID = @ID", new { ID = idUniversity }).FirstOrDefault();
                University university = new University();
                university.NameUniversity = nameUniversity;
                university.BudgetSize = budgetSize;
                university.Rector = _workerAdministratorRepository.Get(idRector);
                university.Administrators = administrators;
                universities.Add(university);
            }
            return universities;
        }
    }
    public int Create(UniversityDto university)
    {
        List<int> idAdministrators = university.IdAdministrators;
        var budget = university.BudgetSize;
        var nameUniversity = university.NameUniversity;
        int rector = university.IdRector;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"INSERT INTO UNIVERSITY VALUES(@nameUniversity, @IdRector, @budget) SELECT MAX(ID) FROM UNIVERSITY";
                    int idUniversity = db.QuerySingle<int>(SqlQuery, new { nameUniversity, IdRector = rector, budget }, transaction);
                    SqlQuery = @"INSERT INTO PersonalOfUniversity VALUES(@IDUniversity, @IDADMINISTRATOR)";
                    db.Execute(SqlQuery,  idAdministrators , transaction);
                    for (int i = 0; i < idAdministrators.Count; i++)
                    {
                        
                    }
                    transaction.Commit();
                    return idUniversity;
                }
                catch (Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                    return -1;
                }
            }
            
        }
    }

    public int Update(Tuple<int, UniversityDto> idAndUniversity)
    {
        int id = idAndUniversity.Item1;
        UniversityDto university = idAndUniversity.Item2;
        List<int> idAdministrators = university.IdAdministrators;
        var budget = university.BudgetSize;
        var nameUniversity = university.NameUniversity;
        int rector = university.IdRector;
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                string SqlQuery;
                try
                {
                    SqlQuery = @"UPDATE UNIVERSITY SET NameUniversity = @nameUniversity, Rector = @rector, Budget = @budget WHERE ID = @id";
                    db.Execute(SqlQuery, new { nameUniversity, rector, budget, id }, transaction);
                    SqlQuery = @"UPDATE PersonalOfUniversity SET IDADMINISTRATOR = @IDADMINISTRATOR WHERE IDUniversity = @id";
                    for (int i = 0; i < idAdministrators.Count; i++)
                    {
                        db.Execute(SqlQuery, new { IDADMINISTRATOR = idAdministrators[i], id }, transaction);
                    }
                    transaction.Commit();
                    _myLogger.Info("Successfully updated universities");
                    return id;
                }
                catch (Exception ex)
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
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            List<int> IdAdministrator =  db.Query<int>(SqsSelectPersonalOfAdministratorQuery + " WHERE IDUniversity = @ID", new { ID }).ToList();
            using (IDbTransaction transaction = db.BeginTransaction())
            {
                try
                {
                    string SqlQuery = @"DELETE FROM PersonalOfUniversity WHERE IDUniversity = @ID";
                    for (int i = 0; i < IdAdministrator.Count; i++)
                    {
                        db.Execute(SqlQuery, new { ID = IdAdministrator[i]}, transaction);
                    }
                    SqlQuery = @"DELETE FROM University WHERE ID = @ID";
                    db.Execute(SqlQuery, new { ID },  transaction);
                    transaction.Commit();
                    _myLogger.Info("Deleted University. ID = " + ID);
                }
                catch (Exception ex)
                {
                    _myLogger.Error("An error occured during transaction" + ex.Message);
                    transaction.Rollback();
                }
            }
        }
    }
    
    private string _connectionString = null;
    private MyLogger _myLogger;
    private IWorkerAdministratorRepository _workerAdministratorRepository;
}
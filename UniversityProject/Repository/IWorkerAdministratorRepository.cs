namespace Repository;
using UCore;
using Logger;

public interface IWorkerAdministratorRepository
{
    
    public void AddAdministrator(Administrator worker, MyLogger myLogger);

    public void PrintAll(MyLogger myLogger);

    public List<Administrator> ReturnListAdministrator(MyLogger myLogger);
}
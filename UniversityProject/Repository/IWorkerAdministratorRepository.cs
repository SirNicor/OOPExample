namespace Repository;
using UCore;
using Logger;

public interface IWorkerAdministratorRepository
{
    public void PrintAll();
    public long Create(Administrator administrator);
    public List<Administrator> ReturnListAdministrator();
    Administrator Get(long ID);
    public void Delete(long Id);
    public long Update(Administrator idAndAdministrator);
}
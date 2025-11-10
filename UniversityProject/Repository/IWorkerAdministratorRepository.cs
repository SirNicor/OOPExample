namespace Repository;
using UCore;
using Logger;

public interface IWorkerAdministratorRepository
{
    public void PrintAll();
    public int Create(Administrator administrator);
    public List<Administrator> ReturnListAdministrator();
    Administrator Get(int ID);
    public void Delete(int ID);
    public int Update(Tuple<int, Administrator> idAndAdministrator);
}
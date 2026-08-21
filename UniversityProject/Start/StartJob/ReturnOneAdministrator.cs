using Repository;

namespace Start;
using UCore;
using IRepositoryAll;

public class ReturnOneAdministrator(IWorkerAdministratorRepository administratorRepository)
{
    public Administrator ReturnAdministrator(int id)
    {
        return administratorRepository.Get(id);
    }
}
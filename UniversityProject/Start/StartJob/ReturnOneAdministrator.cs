using Repository;

namespace Start;
using UCore;

public class ReturnOneAdministrator
{
    private IWorkerAdministratorRepository _administratorRepositoryRepository;
    public ReturnOneAdministrator(IWorkerAdministratorRepository administratorRepository)
    {
        _administratorRepositoryRepository = administratorRepository;   
    }

    public Administrator ReturnAdministrator(int id)
    {
        return _administratorRepositoryRepository.Get(id);
    }
}
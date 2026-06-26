
namespace Start;
using UCore;
using Repository;
using IRepositoryAll;

public class ReturnListAdministrator
{
    private IWorkerAdministratorRepository _administratorRepositoryRepository;
    public ReturnListAdministrator(IWorkerAdministratorRepository administratorRepository)
    {
        _administratorRepositoryRepository = administratorRepository;   
    }

    public List<Administrator> ReturnAdministrator(int id)
    {
        return _administratorRepositoryRepository.ReturnListAdministrator();
    }
}
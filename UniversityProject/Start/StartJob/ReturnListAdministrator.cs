
namespace Start;
using UCore;
using Repository;
using IRepositoryAll;

public class ReturnListAdministrator(IWorkerAdministratorRepository administratorRepository)
{
    private IWorkerAdministratorRepository _administratorRepositoryRepository = administratorRepository;

    public List<Administrator> ReturnAdministrator(int id)
    {
        return _administratorRepositoryRepository.ReturnListAdministrator();
    }
}
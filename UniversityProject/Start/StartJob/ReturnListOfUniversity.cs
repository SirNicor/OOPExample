namespace Start;
using Repository;
using UCore;

public class ReturnListOfUniversity
{
    private IUniversityRepository _universityRepositoryRepository;
    public ReturnListOfUniversity(IUniversityRepository universityRepository)
    {
        _universityRepositoryRepository = universityRepository;   
    }

    public List<University> ReturnList()
    {
        return _universityRepositoryRepository.ReturnList();
    }
}
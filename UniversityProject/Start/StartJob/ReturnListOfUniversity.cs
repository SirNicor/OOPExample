namespace Start;
using Repository;
using UCore;
using IRepositoryAll;

public class ReturnListOfUniversity(IUniversityRepository universityRepository)
{
    public List<University> ReturnList()
    {
        return universityRepository.ReturnList();
    }
}
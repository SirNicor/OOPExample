namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;

public class ReturnOneUniversity
{
    private IUniversityRepository _universityRepositoryRepository;
    public ReturnOneUniversity(IUniversityRepository universityRepository)
    {
        _universityRepositoryRepository = universityRepository;   
    }

    public University ReturnUniversity(int id)
    {
        return _universityRepositoryRepository.Get(id);
    }
}
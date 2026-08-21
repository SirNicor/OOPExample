namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;
using IRepositoryAll;

public class ReturnOneUniversity(IUniversityRepository universityRepository)
{
    public University ReturnUniversity(int id)
    {
        return universityRepository.Get(id);
    }
}
namespace Repository;
using UCore;
using Logger;

public interface IUniversityRepository
{
    public long Create(UniversityDto university);
    public University Get(long ID);
    public long? CheckNameInUniversity(string nameUniversity);
    public List<University> ReturnList();
    public void Delete(long ID);
    public long Update(UniversityDto university);
}
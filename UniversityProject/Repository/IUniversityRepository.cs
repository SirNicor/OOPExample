namespace Repository;
using UCore;
using Logger;

public interface IUniversityRepository
{
    public int Create(UniversityForDB university);
    public University Get(int ID);
    public List<University> ReturnList();
    public void Delete(int ID);
}
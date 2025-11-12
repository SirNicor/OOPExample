namespace Repository;
using UCore;
using Logger;

public interface IUniversityRepository
{
    public int Create(UniversityDto university);
    public University Get(int ID);
    public List<University> ReturnList();
    public void Delete(int ID);
    public int Update(Tuple<int, UniversityDto> idAndUniversity);
}
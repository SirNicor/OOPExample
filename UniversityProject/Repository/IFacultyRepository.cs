namespace Repository;
using UCore;
using Logger;

public interface IFacultyRepository
{
    public long Create(FacultyDto facutly);
    public Faculty Get(long Id);
    public List<Faculty> ReturnList();
    public void Delete(long ID);
    public long Update(FacultyDto faculty);
}
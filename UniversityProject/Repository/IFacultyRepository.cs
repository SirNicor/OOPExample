namespace Repository;
using UCore;
using Logger;

public interface IFacultyRepository
{
    public void Add(Faculty faculty, MyLogger myLogger);
    public List<Faculty> ReturnList(MyLogger myLogger);
}
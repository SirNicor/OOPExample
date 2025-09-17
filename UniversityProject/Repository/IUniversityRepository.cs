namespace Repository;
using UCore;
using Logger;

public interface IUniversityRepository
{
    public void Add(ClassUniversity university, MyLogger myLogger);
    public List<ClassUniversity> ReturnList(MyLogger myLogger);
}
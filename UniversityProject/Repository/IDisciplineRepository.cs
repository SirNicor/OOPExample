namespace Repository;
using UCore;
using Logger;


public interface IDisciplineRepository
{
    public void Add(Discipline discipline, MyLogger myLogger);
    public List<Discipline> ReturnList(MyLogger myLogger);
}
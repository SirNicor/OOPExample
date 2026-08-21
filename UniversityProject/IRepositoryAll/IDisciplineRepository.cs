namespace IRepositoryAll;
using UCore;
using Logger;


public interface IDisciplineRepository
{
    public long Create(DisciplineDto discipline);
    public Discipline Get(long id);
    public List<Discipline> ReturnList();
    public void Delete(long id);
    public long Update(DisciplineDto discipline);
}
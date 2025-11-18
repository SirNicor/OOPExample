namespace Repository;
using UCore;
using Logger;


public interface IDisciplineRepository
{
    public long Create(DisciplineDto discipline);
    public Discipline Get(long Id);
    public List<Discipline> ReturnList();
    public void Delete(long Id);
    public long Update(DisciplineDto discipline);
}
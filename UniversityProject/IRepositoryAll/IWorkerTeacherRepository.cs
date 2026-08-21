namespace IRepositoryAll;
using UCore;
using Logger;

public interface IWorkerTeacherRepository
{
    public void PrintAll();
    public long Create(Teacher teacher);
    public List<Teacher> ReturnList();
    Teacher Get(long id);
    public void Delete(long id);
    public long Update(Teacher teacher);
    
}
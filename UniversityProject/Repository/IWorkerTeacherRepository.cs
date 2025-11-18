namespace Repository;
using UCore;
using Logger;

public interface IWorkerTeacherRepository
{
    public void PrintAll();
    public long Create(Teacher teacher);
    public List<Teacher> ReturnList();
    Teacher Get(long Id);
    public void Delete(long Id);
    public long Update(Teacher teacher);
    
}
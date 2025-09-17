namespace Repository;
using UCore;
using Logger;

public interface IWorkerTeacherRepository
{
    public void Add(Teacher worker, MyLogger myLogger);
    public void PrintAll(MyLogger myLogger);
    public List<Teacher> ReturnListTeachers(MyLogger myLogger);
    
}
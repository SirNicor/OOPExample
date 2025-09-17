namespace Repository;
using UCore;
using Logger;

public interface IStudentRepository
{
    public void Add(Student student, MyLogger myLogger);
    public void PrintAll(MyLogger myLogger);
    public List<Student> ReturnList(MyLogger myLogger);
}
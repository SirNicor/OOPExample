namespace Repository;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public void PrintAll(MyLogger myLogger);
    public List<Student> ReturnList(MyLogger myLogger);
    void Create(Student student, MyLogger myLogger);
    void Update(Student student, MyLogger myLogger);
    void Delete(int ID, MyLogger myLogger);
    Student Get(int ID, MyLogger myLogger);
}
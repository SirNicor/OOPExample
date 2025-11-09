namespace Repository;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public void PrintAll();
    public List<Student> ReturnList();
    int Create(Student student);
    int Update(Tuple<int, Student> student);
    void Delete(int ID);
    void DeleteAddress(int ID);
    void DeletePassport(int ID);
    Student Get(int ID);
}
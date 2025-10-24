namespace Repository;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public void PrintAll();
    public List<Student> ReturnList();
    void Create(Student student);
    void Update(Student student);
    void Delete(int ID);
    void DeleteAddress(int ID);
    void DeletePassport(int ID);
    Student Get(int ID);
}
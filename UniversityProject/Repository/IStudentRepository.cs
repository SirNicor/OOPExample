namespace Repository;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public void PrintAll();
    public List<Student> ReturnList();
    long Create(Student student);
    long Update(Student student);
    void Delete(long ID);
    void DeleteAddress(long ID);
    void DeletePassport(long ID);
    Student Get(long ID);
}
using System.Collections;
using Repository;
using UCore;
using Repository;
using Logger;

namespace Start;

public class ReturnListOfStudents 
{
    private IStudentRepository _studentRepository;
    public ReturnListOfStudents(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;   
    }

    public List<Student> ReturnList()
    {
        return _studentRepository.ReturnList();
    }
}
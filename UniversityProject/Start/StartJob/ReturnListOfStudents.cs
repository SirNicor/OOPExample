using System.Collections;
using Repository;
using UCore;
using Repository;
using Logger;

namespace Start;

public class ReturnListOfStudents 
{
    private IStudentRepository _studentRepository;
    private MyLogger _loggerMain;
    public ReturnListOfStudents(IStudentRepository studentRepository, MyLogger loggerMain)
    {
        _studentRepository = studentRepository;   
        _loggerMain = loggerMain;
    }

    public List<Student> ReturnList()
    {
        return _studentRepository.ReturnList(_loggerMain);
    }
}
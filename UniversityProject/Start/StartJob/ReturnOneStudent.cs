namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;

public class ReturnOneStudent
{
    private IStudentRepository _studentRepository;
    private MyLogger _loggerMain;
    public ReturnOneStudent(IStudentRepository studentRepository, MyLogger loggerMain)
    {
        _studentRepository = studentRepository;   
        _loggerMain = loggerMain;
    }

    public Student ReturnStudent(int id)
    {
        return _studentRepository.Get(id, _loggerMain);
    }
}
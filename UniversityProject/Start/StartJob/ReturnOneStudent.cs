namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;

public class ReturnOneStudent
{
    private IStudentRepository _studentRepository;
    public ReturnOneStudent(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;   
    }

    public Student ReturnStudent(int id)
    {
        return _studentRepository.Get(id);
    }
}
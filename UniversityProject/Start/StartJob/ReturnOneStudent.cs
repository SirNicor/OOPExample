namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;
using IRepositoryAll;

public class ReturnOneStudent
{
    private IStudentRepository _studentRepository;
    public ReturnOneStudent(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;   
    }

    public async Task<Student> ReturnStudentAsync(int id)
    {
        return await _studentRepository.GetAsync(id);
    }
}
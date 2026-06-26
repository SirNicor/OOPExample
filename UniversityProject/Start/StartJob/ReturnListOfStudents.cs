using System.Collections;
using Repository;
using UCore;
using Repository;
using Logger;
using IRepositoryAll;

namespace Start;

public class ReturnListOfStudents 
{
    private IStudentRepository _studentRepository;
    public ReturnListOfStudents(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;   
    }

    public async Task<List<Student>> ReturnListAsync()
    {
        return await _studentRepository.ReturnListAsync();
    }
}
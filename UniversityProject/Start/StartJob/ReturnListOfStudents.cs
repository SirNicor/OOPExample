using System.Collections;
using Repository;
using UCore;
using Repository;
using Logger;
using IRepositoryAll;

namespace Start;

public class ReturnListOfStudents(IStudentRepository studentRepository)
{
    public async Task<List<Student>> ReturnListAsync()
    {
        return await studentRepository.ReturnListAsync();
    }
}
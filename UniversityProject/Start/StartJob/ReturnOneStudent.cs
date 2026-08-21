namespace Start;
using Repository;
using UCore;
using Repository;
using Logger;
using IRepositoryAll;

public class ReturnOneStudent(IStudentRepository studentRepository)
{
    public async Task<Student> ReturnStudentAsync(int id)
    {
        return await studentRepository.GetAsync(id);
    }
}
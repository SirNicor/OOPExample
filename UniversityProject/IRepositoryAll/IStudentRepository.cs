namespace IRepositoryAll;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public Task PrintAllAsync();
    public Task<List<Student>> ReturnListAsync();
    Task<long> CreateAsync(StudentDtoForPage student, CancellationToken token);
    Task<long?> UpdateAsync(StudentDtoForPage student, CancellationToken token);
    Task DeleteAsync(long ID, CancellationToken token);
    Task DeleteAddressAsync(long ID);
    Task DeletePassportAsync(long ID);
    Task<Student> GetAsync(long ID);
    Task<StudentDtoForPage> GetStudentPageAsync(long studentId, CancellationToken token);
    Task<(List<StudentTableDTO>, long)> GetStudentTableDTO(long FirstId, long count, string? SortColumn,
        string? SortOrder, FilterDto? filter, CancellationToken token);
    Task<long> GetCountAsync(CancellationToken token);
    public Task<long?> CheckNameAsync(string firstName, string LastName);
}
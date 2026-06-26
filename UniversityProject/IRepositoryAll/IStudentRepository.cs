namespace IRepositoryAll;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public Task PrintAllAsync();
    public Task<List<Student>> ReturnListAsync();
    Task<long> CreateAsync(StudentDtoForPage student);
    Task<long?> UpdateAsync(StudentDtoForPage student);
    Task DeleteAsync(long ID);
    Task DeleteAddressAsync(long ID);
    Task DeletePassportAsync(long ID);
    Task<Student> GetAsync(long ID);
    Task<StudentDtoForPage> GetStudentPageAsync(long studentId);
    Task<(List<StudentTableDTO>, long)> GetStudentTableDTO(long FirstId, long count, string? SortColumn,
        string? SortOrder, FilterDto? filter);
    Task<long> GetCountAsync();
    public Task<Student?> GetStudentForChatIdAsync(string chatId);
    public Task<long?> CheckNameAsync(string firstName, string LastName);
}
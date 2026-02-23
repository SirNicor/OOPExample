namespace Repository;
using UCore;
using Logger;
using System.Collections.Generic;

public interface IStudentRepository
{
    public void PrintAll();
    public List<Student> ReturnList();
    long Create(StudentDtoForPage student);
    long? Update(StudentDtoForPage student);
    void Delete(long ID);
    void DeleteAddress(long ID);
    void DeletePassport(long ID);
    Student Get(long ID);
    StudentDtoForPage GetStudentPage(long studentId);
     Tuple<List<StudentTableDTO>, long>GetStudentTableDTO(long FirstId, long count, string? SortColumn,
        string? SortOrder, FilterDto? filter);
    long GetCount();
    public Student? GetStudentForChatId(string chatId);
    public long? CheckName(string firstName, string LastName);
}
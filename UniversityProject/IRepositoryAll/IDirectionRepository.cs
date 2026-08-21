namespace IRepositoryAll;
using UCore;
using Logger;

public interface IDirectionRepository
{
    public long Create(DirectionDto direction);
    public Direction Get(long id);
    public List<Direction> ReturnList();
    public void Delete(long id);
    public long Update(DirectionDto direction);
    public long? CheckNameDirection(string nameDirection, long departmentId);
    public long AuthorizationVerification(long chatId);
    public bool CheckStudent(long studentId);
}   
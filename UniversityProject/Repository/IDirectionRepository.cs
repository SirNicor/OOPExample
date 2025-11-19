namespace Repository;
using UCore;
using Logger;

public interface IDirectionRepository
{
    public long Create(DirectionDto direction);
    public Direction Get(long Id);
    public List<Direction> ReturnList();
    public void Delete(long ID);
    public long Update(DirectionDto direction);
}
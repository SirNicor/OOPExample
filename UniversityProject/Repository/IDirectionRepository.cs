namespace Repository;
using UCore;
using Logger;

public interface IDirectionRepository
{
    public void Add(Direction direction, MyLogger myLogger);
    public List<Direction> ReturnList(MyLogger myLogger);
}
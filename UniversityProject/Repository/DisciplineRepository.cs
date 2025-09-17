namespace Repository;
using UCore;
using Logger;

public class DisciplineRepository : IDisciplineRepository
{
    public DisciplineRepository(MyLogger myLogger,IDirectionRepository directionRepository)
    {
        _directions = directionRepository;
        List<Direction> directions = _directions.ReturnList(myLogger);
        for (int i = 0; i < 12; i++)
        {
            Discipline discipline = new Discipline($"Discipline{i}", directions);
            _disciplines.Add(discipline);
        }
        Console.WriteLine(_disciplines.Count);
    }

    public void Add(Discipline discipline, MyLogger myLogger)
    {
        try
        {
            _disciplines.Add(discipline);
            myLogger.Debug("Discipline added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Discipline not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Discipline> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _disciplines;
    }   
    
    private static List<Discipline> _disciplines = new  List<Discipline>();
    private static IDirectionRepository  _directions;
}
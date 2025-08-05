namespace Repository;
using UCore;
using Logger;

public class DisciplineRepository
{
    public DisciplineRepository(DirectionRepository directionRepository)
    {
        _directions = directionRepository;
        List<Direction> directions = _directions.ReturnList();
        for (int i = 0; i < 12; i++)
        {
            Discipline discipline = new Discipline($"Discipline{i}", directions);
            _disciplines.Add(discipline);
        }
        Console.WriteLine(_disciplines.Count);
    }

    public void Add(Discipline discipline, Logger logger)
    {
        try
        {
            _disciplines.Add(discipline);
            logger.Debug("Discipline added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Discipline not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Discipline> ReturnList(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _disciplines;
    }   
    
    internal List<Discipline> ReturnList()
    {
        return _disciplines;
    }
    
    private static List<Discipline> _disciplines = new  List<Discipline>();
    private static DirectionRepository  _directions;
}
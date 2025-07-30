namespace Repository;
using University.UCore;
using Logger;

public class DisciplineRepository
{
    static DisciplineRepository()
    {
        
    }

    public DisciplineRepository()
    {
        Discipline discipline = new Discipline("Discipline1");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline2");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline3");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline4");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline5");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline6");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline7");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline8");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline9");
        _disciplines.Add(discipline);
        discipline = new Discipline("Discipline10");
        _disciplines.Add(discipline);
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
}
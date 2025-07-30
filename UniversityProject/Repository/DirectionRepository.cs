namespace Repository;
using University.UCore;
using Logger;

public class DirectionRepository
{
    static DirectionRepository()
    {
        _departmentRepository = new DepartmentRepository();
        _disciplineRepository = new DisciplineRepository();
        _studentRepository = new StudentRepository();
        Direction direction = new Direction(_departmentRepository.ReturnList()[0], "Direction1", 
            DegreesStudy.bachelor, _disciplineRepository.ReturnList().GetRange(0, 10)
            , _studentRepository.ReturnList().GetRange(0, 10));
        _directions.Add(direction);
    }

    public DirectionRepository()
    {
        
    }
    
    public void Add(Direction direction, Logger logger)
    {
        try
        {
            _directions.Add(direction);
            logger.Debug("Direction added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            logger.Error("Direction not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Direction> ReturnList(Logger logger)
    {
        logger.Debug("Return list" + Environment.NewLine);
        return _directions;
    }
    
    internal List<Direction> ReturnList()
    {
        return _directions;
    }

    private static DisciplineRepository _disciplineRepository;
    private static StudentRepository _studentRepository;
    private static DepartmentRepository _departmentRepository;
    private static List<Direction> _directions = new List<Direction>();
    private static Logger _logger;
}
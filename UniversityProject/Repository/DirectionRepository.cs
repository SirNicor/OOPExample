namespace Repository;
using UCore;
using Logger;

public class DirectionRepository
{
    public DirectionRepository(Logger logger, StudentRepository studentRepository, DepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
        _studentRepository = studentRepository;
        Direction direction = new Direction(_departmentRepository.ReturnList()[0], "Direction1", 
            DegreesStudy.bachelor, _studentRepository.ReturnList(logger).GetRange(0, 10));
        _directions.Add(direction);
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

    private static StudentRepository _studentRepository;
    private static DepartmentRepository _departmentRepository;
    private static List<Direction> _directions = new List<Direction>();
    private static Logger _logger;
}
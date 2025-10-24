namespace Repository;
using UCore;
using Logger;

public class DirectionRepository : IDirectionRepository
{
    public DirectionRepository(MyLogger myLogger, IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
        _studentRepository = studentRepository;
        Direction direction = new Direction(_departmentRepository.ReturnList(myLogger)[0], "Direction1", 
            DegreesStudy.bachelor, _studentRepository.ReturnList().GetRange(0, 5));
        _directions.Add(direction);
    }
    
    public void Add(Direction direction, MyLogger myLogger)
    {
        try
        {
            _directions.Add(direction);
            myLogger.Debug("Direction added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Direction not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Direction> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _directions;
    }

    private static IStudentRepository _studentRepository;
    private static IDepartmentRepository _departmentRepository;
    private static List<Direction> _directions = new List<Direction>();
    private static MyLogger _myLogger;
}
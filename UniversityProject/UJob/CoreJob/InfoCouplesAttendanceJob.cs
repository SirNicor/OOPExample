namespace UJob;
using Repository;
using Logger;
using UCore;

public class InfoCouplesAttendanceJob
{
    public InfoCouplesAttendanceJob(Logger logger, StudentRepository studentRepository)
    {
        _logger = logger;
        _students = studentRepository.ReturnList(logger);
    }

    public void DoWork()
    {
        int[] couplesAttendance = new int[_students.Count];
        int maxindex = 0, minindex = 0;
        for(int i = 0; i < _students.Count; i++)
        {
            couplesAttendance[i] = _students[i].SkipHours;
            Console.WriteLine(couplesAttendance[i]);
            if (couplesAttendance[maxindex] < couplesAttendance[i])
            {
                maxindex = i;
            }
            if (couplesAttendance[minindex] < couplesAttendance[i])
            {
                minindex = i;
            }
        }
        _logger.Info($"Максимально количество пропусков= {couplesAttendance[maxindex]}. Данный студент:");
        _students[maxindex].PrintInfo(_logger);
        _logger.Info($"Минимальное количество пропусков = {couplesAttendance[minindex]}. Данный студент:");
        _students[minindex].PrintInfo(_logger);
    }
    
    
    private readonly Logger _logger;
    private Timer _timer;
    private List<Student> _students;
}
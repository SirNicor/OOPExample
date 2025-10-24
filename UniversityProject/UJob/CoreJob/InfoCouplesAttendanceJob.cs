namespace UJob;
using Repository;
using Logger;
using UCore;

public class InfoCouplesAttendanceJob:IJob,  IInfoCouplesAttendanceJob
{
    public InfoCouplesAttendanceJob(MyLogger myLogger, IStudentRepository studentRepository)
    {
        _myLogger = myLogger;
        _students = studentRepository.ReturnList();
    }

    public void DoWork()    
    {
        int?[] couplesAttendance = new int?[_students.Count];
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
        _myLogger.Info($"Максимально количество пропусков= {couplesAttendance[maxindex]}. Данный студент:");
        _students[maxindex].PrintInfo(_myLogger);
        _myLogger.Info($"Минимальное количество пропусков = {couplesAttendance[minindex]}. Данный студент:");
        _students[minindex].PrintInfo(_myLogger);
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private List<Student> _students;
}
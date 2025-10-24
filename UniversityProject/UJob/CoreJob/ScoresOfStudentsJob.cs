namespace UJob;
using Repository;
using Logger;
using UCore;


public class ScoresOfStudentsJob:IJob, IScoresOfStudentsJob
{
    public ScoresOfStudentsJob(MyLogger myLogger, IStudentRepository studentRepository)
    {
        _myLogger = myLogger;
        _students = studentRepository.ReturnList();
    }

    public void DoWork()
    {
        double?[] scoreStudent = new double?[_students.Count];
        int maxindex = 0, minindex = 0;
        double? allscores = 0;
        for(int i = 0; i < _students.Count; i++)
        {
            scoreStudent[i] = _students[i].TotalScore;
            Console.WriteLine(scoreStudent[i]);
            if (scoreStudent[maxindex] < scoreStudent[i])
            {
                maxindex = i;
            }
            if (scoreStudent[minindex] < scoreStudent[i])
            {
                minindex = i;
            }
            allscores += scoreStudent[i];
        }
        
        
        _myLogger.Info($"Максимальные баллы = {scoreStudent[maxindex]}. Студент с данными баллами:");
        _students[maxindex].PrintInfo(_myLogger);
        _myLogger.Info($"Минимальные баллы = {scoreStudent[minindex]}. Студент с данными баллами:");
        _students[minindex].PrintInfo(_myLogger);
        _myLogger.Info("Средние баллы = " + allscores/_students.Count);
    }
    
    
    private readonly MyLogger _myLogger;
    private Timer _timer;
    private List<Student> _students;
}
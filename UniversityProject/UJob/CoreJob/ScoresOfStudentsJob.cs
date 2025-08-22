namespace UJob;
using Repository;
using Logger;
using UCore;


public class ScoresOfStudentsJob
{
    public ScoresOfStudentsJob(Logger logger, StudentRepository studentRepository)
    {
        _logger = logger;
        _students = studentRepository.ReturnList(logger);
    }

    public void DoWork()
    {
        double[] scoreStudent = new double[_students.Count];
        int maxindex = 0, minindex = 0;
        double allscores = 0;
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
        
        
        _logger.Info($"Максимальные баллы = {scoreStudent[maxindex]}. Студент с данными баллами:");
        _students[maxindex].PrintInfo(_logger);
        _logger.Info($"Минимальные баллы = {scoreStudent[minindex]}. Студент с данными баллами:");
        _students[minindex].PrintInfo(_logger);
        _logger.Info("Средние баллы = " + allscores/_students.Count);
    }
    
    
    private readonly Logger _logger;
    private Timer _timer;
    private List<Student> _students;
}
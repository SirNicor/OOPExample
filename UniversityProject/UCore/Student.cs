using System.Net.Security;
using Logger;

namespace UCore;
using Logger;
public class Student:Person
{
    protected const int MinChances = 200;
    public double? TotalScore {
        get
        {
            if (CountOfExamsPassed == 0)
            {
                return 0;
            } 
            else
            {
                return ((double?)CreditScores/CountOfExamsPassed)/5*100;
            }
        }
    }

    public override void PrintDerivedClass(MyLogger myLogger)
    {
        string message = $"Course: {Course}" + Environment.NewLine;
        message += $"Общий балл ={CreditScores} и количество сданных экзаменов = {CountOfExamsPassed} и общий балл = {TotalScore}" + Environment.NewLine;
        // message += "Расположен ли в общежитии " + (_accomodationDormitories ? "Да" : "Нет");
        myLogger.Info(message);
    }

    
    public int? SkipHours
    {
        get { return _skipHours;}
        set
        {
            if (value == null)
            {
                _skipHours = 0;
            }
            else if (value < 0)
            {
                _skipHours += 0;
            }
            else
            {
                _skipHours += value*2;
            }
        }
    }
    
    public int? CreditScores
    {
        get { return _creditScores;}
        set
        {
            if (value == null)
            {
                _creditScores = 0;
            }
            else if (value < 0)
            {
                _creditScores -= 1;
            }
            else
            {
                _creditScores += value ;
            }
        }
    }

    public void NextExamsPassed()
    {
        CountOfExamsPassed++;
    }
    public int? CountOfExamsPassed
    {
        get{return _countOfExamsPassed;}
        set
        {
            if (value == null)
            {
                _countOfExamsPassed = 0;
            }
            else 
            {
                _countOfExamsPassed = value;
            }
        }
    }
    public int? Course
    {
        get { return _course;} 
        set{ _course=value;} 
    }
    public string? ChatId { get; set; }
    private int? _course;
    private int? _countOfExamsPassed;
    private int? _skipHours;
    private int? _creditScores;
    

    // private bool _accomodationDormitories;

}




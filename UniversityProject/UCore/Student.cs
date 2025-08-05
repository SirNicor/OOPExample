using System.Net.Security;
using Logger;

namespace UCore;
using Logger;
public class Student:Person
{
    protected const int MinChances = 200;
    public Student(Passport passport, IdMillitary militaryIdAvailability, bool criminalRecord, int course, bool accomodationDormitories, DegreesStudy degreesStudy) :
        base(passport, militaryIdAvailability, criminalRecord)
    {
        _course = CheckMethods.CheckDegress(course, degreesStudy);
        _skipHours = 0;
        _countOfExamsPassed = 0;
        _creditScores = 0;
        _accomodationDormitories =  accomodationDormitories;
    }
    public double TotalScore {
        get
        {
            if (_countOfExamsPassed == 0)
            {
                return 0;
            }
            else
            {
                return ((double)_creditScores/_countOfExamsPassed)/5*100;
            }
        }
    }

    public override void PrintDerivedClass(Logger logger)
    {
        string message = $"Course: {_course}" + Environment.NewLine;
        message += $"Общий балл ={_creditScores} и количество сданных экзаменов = {_countOfExamsPassed} и общий балл = {TotalScore}" + Environment.NewLine;
        message += "Расположен ли в общежитии " + (_accomodationDormitories ? "Да" : "Нет");
        logger.Info(message);
    }

    
    public int SkipHours
    {
        get { return _skipHours;}
        set
        {
            if (value < 0)
            {
                _skipHours += 0;
            }
            else
            {
                _skipHours += value*2;
            }
        }
    }
    
    public int CreditScores
    {
        get { return _creditScores;}
        set
        {
            if (value + _creditScores > 0)
            {
                _creditScores -= 1;
            }
            else
            {
                _creditScores += value ;
            }
        }
    }

    public int CountOfExamsPassed
    {
        get{return _countOfExamsPassed;}
        set
        {
            _countOfExamsPassed += 1;
        }
    }
    protected Student(){}
    private int _creditScores;
    private int _countOfExamsPassed;
    private int _skipHours;
    private int _course;
    private bool _accomodationDormitories;
    
}





namespace Repository;
using UCore;
using Logger;

public class StudentRepository
{
    public StudentRepository(MyLogger myLogger)
    {
        try
        {
            Passport passport;
            Student student;
            passport = new Passport(1111, 123456, "StudentFirstName1", "StudentLastName1",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "1");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName2", "StudentLastName2",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "2");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName3", "StudentLastName3",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "3");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName4", "StudentLastName4",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "4");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName5", "StudentLastName5",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "5");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName6", "StudentLastName6",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "6");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName7", "StudentLastName7",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "7");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName8", "StudentLastName8",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "8");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName9", "StudentLastName9",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "9");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
            passport = new Passport(1111, 123456, "StudentFirstName10", "StudentLastName10",
                new DateTime(2000, 01, 01), new Address("Russia", "city",
                    "street", 1), "10");
            student = new Student(passport, IdMillitary.DidNotServe, false, 1, false, DegreesStudy.bachelor);
            _student.Add(student);
        }
        catch (Exception ex)
        {
            myLogger.Error("StudentRepError", ex);
        }
        
    }
    
    public void Add(Student student, MyLogger myLogger)
    {
        try
        {
            _student.Add(student);
            myLogger.Debug("Student added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Student not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }

    public void PrintAll(MyLogger myLogger)
    {
        foreach (Student student in _student)
        {
            student.PrintInfo(myLogger);
        }
    }

    public List<Student> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _student;
    }
    
    private static List<Student> _student = new List<Student>(0);
}
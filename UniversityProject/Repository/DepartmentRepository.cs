﻿namespace Repository;
using UCore;
using Logger;

public class DepartmentRepository
{
    public DepartmentRepository(MyLogger myLogger, FacultyRepository facultyRepository, WorkerRepository workerRepository)
    {
        _facultyRepository = facultyRepository;
        WorkerRepository workerRep = workerRepository;
        foreach (var worker in workerRep.ReturnListAdministrator(myLogger))
        {
            if (worker.GetType() == typeof(Administrator))
            {
                _workerRep.Add((Administrator)worker);
            }
        } 
        _departments.Add(new Department("IIST", _workerRep[5],  _workerRep.GetRange(6, 1), _facultyRepository.ReturnList()[0]));
    }
    
    public void Add(Department department, MyLogger myLogger)
    {
        try
        {
            _departments.Add(department);
            myLogger.Debug("Worker added" + Environment.NewLine);
        }
        catch(Exception exception)
        {
            myLogger.Error("Worker not added, The information is incomplete " + Environment.NewLine, exception);
        }
    }
    
    public List<Department> ReturnList(MyLogger myLogger)
    {
        myLogger.Debug("Return list" + Environment.NewLine);
        return _departments;
    }
    
    internal List<Department> ReturnList()
    {
        return _departments;
    }

    private static FacultyRepository _facultyRepository;
    private static List<Administrator> _workerRep = new List<Administrator>();
    private static List<Department> _departments = new List<Department>();
}
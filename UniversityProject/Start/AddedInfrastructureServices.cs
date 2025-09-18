using Logger;

namespace Start;
using UCore;
using Repository;
using Logger;
using UJob;

public class AddedInfrastructureServices
{
    public static void AddInfrastructureServices(IServiceCollection services, MyLogger logger)
    {
        services.AddSingleton<MyLogger>(logger);
        services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
        services.AddSingleton<IStudentRepository, StudentRepository>();
        services.AddSingleton<IDirectionRepository, DirectionRepository>();
        services.AddSingleton<IDisciplineRepository,  DisciplineRepository>();
        services.AddSingleton<IFacultyRepository, FacultyRepository>();
        services.AddSingleton<IUniversityRepository, UniversityRepository>();
        services.AddSingleton<IWorkerTeacherRepository, WorkerTeacherRepository>();
        services.AddSingleton<IWorkerAdministratorRepository, WorkerAdministratorRepository>();
        services.AddSingleton<IInfoCouplesAttendanceJob, InfoCouplesAttendanceJob>();
        services.AddSingleton<IPrintStudentJob, PrintStudentsJob>();
        services.AddSingleton<IPrintWorkersJob, PrintWorkersJob>();
        services.AddSingleton<ISalaryJob, SalaryJob>();
        services.AddSingleton<IScoresOfStudentsJob, ScoresOfStudentsJob>();
    }
}
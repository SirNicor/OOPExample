using ApiTelegramBot;

namespace Start;
using Repository;
using Logger;
using UJob;

public static class AddedInfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, MyLogger logger, IConfiguration configuration)
    {
        services.AddSingleton<MyLogger>(logger);
        services.AddSingleton<IGetConnectionString, GetConnectionString>();
        services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
        services.AddSingleton<IStudentRepository, StudentRepository>();
        services.AddSingleton<IDirectionRepository, DirectionRepository>();
        services.AddSingleton<IDisciplineRepository,  DisciplineRepository>();
        services.AddSingleton<IFacultyRepository, FacultyRepository>();
        services.AddSingleton<IUniversityRepository, UniversityRepository>();
        services.AddSingleton<IScheduleRepository, ScheduleRepository>();
        services.AddSingleton<IWorkerTeacherRepository, WorkerTeacherRepository>();
        services.AddSingleton<IWorkerAdministratorRepository, WorkerAdministratorRepository>();
        services.AddTransient<IInfoCouplesAttendanceJob, InfoCouplesAttendanceJob>();
        services.AddTransient<IPrintStudentJob, PrintStudentsJob>();
        services.AddTransient<IPrintWorkersJob, PrintWorkersJob>();
        services.AddTransient<ISalaryJob, UJob.SalaryJob>();
        services.AddTransient<IScoresOfStudentsJob, ScoresOfStudentsJob>();
        services.AddTransient<ReturnListOfStudents>();
        services.AddSingleton<ReturnOneStudent>();
        services.AddTransient<ReturnListAdministrator>();
        services.AddSingleton<ReturnOneAdministrator>();
        services.AddTransient<ReturnListOfUniversity>();
        services.AddSingleton<ReturnOneUniversity>();
        services.AddTransient<IGetToken, GetToken>();
        services.AddSingleton<IStartBot, StartBot>();
        services.AddSingleton<IStartFunctionalForGroup, StartFunctionalForGroup>();
        services.AddTransient<IDisciplineUpdate, DisciplineUpdate>();
        services.AddTransient<IStudentUpdate, StudentUpdate>();
        services.AddSingleton<IInitializedClass, InitializedClass>();
        services.AddSingleton<IRegistrationClass, RegistrationClass>();
        services.AddSingleton<IRegistrationForUniversity, RegistrationForUniversity>();
        services.AddSingleton<IRegistrationForDepartment, RegistrationForDepartment>();
        services.AddSingleton<IRegistrationForFaculty, RegistrationForFaculty>();
        services.AddSingleton<IRegistrationForDirection, RegistrationForDirection>();
        services.AddSingleton<IRegistrationForLastName, RegistrationForLastName>();
        services.AddSingleton<IRegistrationForFirstName, RegistrationForFirstName>();
    }
}
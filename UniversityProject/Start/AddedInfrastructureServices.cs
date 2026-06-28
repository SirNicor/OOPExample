using ApiTelegramBot;
using EFRepository;

namespace Start;
using Repository;
using Logger;
using UJob;
using IRepositoryAll;

public static class AddedInfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, MyLogger logger, IConfiguration configuration)
    {
        services.AddSingleton<MyLogger>(logger);
        services.AddSingleton<IGetConnectionString, GetConnectionString>();
        services.AddSingleton<IScheduleUpdate, ScheduleUpdate>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IStudentRepository, EFStudentRepository>();
        services.AddScoped<IDirectionRepository, DirectionRepository>();
        services.AddScoped<IDisciplineRepository,  DisciplineRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IWorkerTeacherRepository, WorkerTeacherRepository>();
        services.AddScoped<IWorkerAdministratorRepository, WorkerAdministratorRepository>();
        services.AddScoped<IUserStateTelegramRepository, UserStateTelegramRepository>();
        services.AddTransient<IInfoCouplesAttendanceJob, InfoCouplesAttendanceJob>();
        services.AddTransient<IPrintStudentJob, PrintStudentsJob>();
        services.AddTransient<IPrintWorkersJob, PrintWorkersJob>();
        services.AddTransient<ISalaryJob, UJob.SalaryJob>();
        services.AddTransient<IScoresOfStudentsJob, ScoresOfStudentsJob>();
        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
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
        services.AddTransient<ICreateMessageClass, CreateMessageClass>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddSingleton<FunctionOfBot, FunctionOfBot>();
        services.AddSingleton<CreateFileClass, CreateFileClass>();
    }
}
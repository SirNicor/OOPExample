namespace ApiTelegramBot;
using Logger;
using Telegram.Bot;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Logger;
using Repository;
using UCore;
public class FunctionOfBot
{
    public IStartFunctionalForGroup StartFunctionalForGroup;
    public IInitializedClass InitializedClass;
    public IRegistrationClass RegistrationClass;
    public IRegistrationForDepartment RegistrationForDepartment;
    public IRegistrationForLastName RegistrationForLastName;
    public IRegistrationForFirstName RegistrationForFirstName;
    public IRegistrationForUniversity RegistrationForUniversity;
    public IRegistrationForFaculty RegistrationForFaculty;
    public IRegistrationForDirection RegistrationForDirection;
    public IUserStateRepository UserStateRepository;
    public FunctionOfBot(IStartFunctionalForGroup startFunctionalForGroup,
        IInitializedClass initializedClass, IRegistrationClass registrationClass, IRegistrationForFaculty registrationForFaculty,
        IRegistrationForDepartment registrationForDepartment, IRegistrationForDirection registrationForDirection,
        IRegistrationForLastName registrationForLastName, IRegistrationForFirstName registrationForFirstName, 
        IRegistrationForUniversity registrationForUniversity, IUserStateRepository userStateRepository)
    {
        StartFunctionalForGroup = startFunctionalForGroup;
        InitializedClass = initializedClass;
        RegistrationClass = registrationClass;
        RegistrationForDepartment = registrationForDepartment;
        RegistrationForLastName = registrationForLastName;
        RegistrationForFirstName = registrationForFirstName;
        RegistrationForUniversity = registrationForUniversity;
        RegistrationForFaculty = registrationForFaculty;
        RegistrationForDirection =  registrationForDirection;
        UserStateRepository = userStateRepository;
    }
}
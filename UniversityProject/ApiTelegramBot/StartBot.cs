using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.Json;
using System.Text.Encodings;
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
public class StartBot : IStartBot
{
    private TelegramBotClient _botClient;
    private string _token;
    private ReceiverOptions _receiverOptions;
    private MyLogger _logger;
    private IStartFunctionalForGroup _startFunctionalForGroup;
    private IInitializedClass _initializedClass;
    private IRegistrationClass _registrationClass;
    private IRegistrationForDepartment _registrationForDepartment;
    private IRegistrationForLastName _registrationForLastName;
    private IRegistrationForFirstName _registrationForFirstName;
    private IRegistrationForUniversity _registrationForUniversity;
    private IRegistrationForFaculty _registrationForFaculty;
    private IRegistrationForDirection _registrationForDirection;
    private IUserStateRepository _userStateRepository;
    public StartBot(IGetToken getToken, MyLogger logger, IStartFunctionalForGroup startFunctionalForGroup,
        IInitializedClass initializedClass, IRegistrationClass registrationClass, IRegistrationForFaculty registrationForFaculty,
        IRegistrationForDepartment registrationForDepartment, IRegistrationForDirection registrationForDirection,
        IRegistrationForLastName registrationForLastName, IRegistrationForFirstName registrationForFirstName, 
        IRegistrationForUniversity registrationForUniversity, IUserStateRepository userStateRepository)
    {
        _token = getToken.ReturnToken();
        _logger = logger;
        _startFunctionalForGroup = startFunctionalForGroup;
        _initializedClass = initializedClass;
        _registrationClass = registrationClass;
        _registrationForDepartment = registrationForDepartment;
        _registrationForLastName = registrationForLastName;
        _registrationForFirstName = registrationForFirstName;
        _registrationForUniversity = registrationForUniversity;
        _registrationForFaculty = registrationForFaculty;
        _registrationForDirection =  registrationForDirection;
        _userStateRepository = userStateRepository;
    }
    public async Task ListenForMessagesAsync()
    {
        _botClient = new TelegramBotClient(_token);
        using var cts = new CancellationTokenSource();

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
            DropPendingUpdates = true
        };
        var me = await _botClient.GetMe();
        _logger.Info($"{me.FirstName} запущен!");
        await _botClient.ReceiveAsync(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: cts.Token
        );;
        Console.ReadKey();
        cts.Cancel();
    }
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message || message.Text is not { } messageText)
        {
            return;
        }
        var id = message.Chat.Id;
        var type = message.Chat.Type;
        _logger.Info(messageText);
        var userStateReg = _userStateRepository.Get(id);
        if (userStateReg == null)
        {
            userStateReg = new UserStateRegistration(id);
            _userStateRepository.Create(userStateReg);
        }
        IRegistrationClass registrationClass = null;
        switch (type)
        {
            case ChatType.Private:
            {
                switch (userStateReg.UserState)
                {
                    case UserStateRegEnum.notInitialized:
                    {
                        _initializedClass.Initialize(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.forRegistration:
                    {
                        _registrationClass.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForUniversityInput:
                    {   
                        _registrationForUniversity.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForFacultyInput:
                    {
                        _registrationForFaculty.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForDepartmentInput:
                    {
                        _registrationForDepartment.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForDirectionInput:
                    {
                        _registrationForDirection.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForLastNameInput:
                    {
                        _registrationForLastName.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.waitingForFirstNameInput:
                    {
                        _registrationForFirstName.Registration(id, _botClient, messageText, userStateReg);
                        return;
                    }
                    case UserStateRegEnum.fullRegistration:
                    {
                        _startFunctionalForGroup.Functional(id, messageText ,_botClient, ChatType.Private);
                        return;
                    }
                }
                return;
            }
            case ChatType.Group:
            {
                _startFunctionalForGroup.Functional(id, messageText ,_botClient, ChatType.Group);
                return;
            }
        }
    }
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
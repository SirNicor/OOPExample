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
    private IStartUpdate _startUpdate;
    private IDisciplineUpdate _disciplineUpdate;
    private IStudentUpdate _studentUpdate;
    public StartBot(IGetToken getToken, MyLogger logger, IStartUpdate startUpdate, 
        IDisciplineUpdate disciplineUpdate, IStudentUpdate studentUpdate)
    {
        _token = getToken.ReturnToken();
        _logger = logger;
        _startUpdate = startUpdate;
        _disciplineUpdate = disciplineUpdate;
        _studentUpdate = studentUpdate;
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
        switch (update.Type)
        {
            case UpdateType.Message:
            {
                switch (message.Type)
                {
                    case MessageType.Text:
                    {
                        switch (message.Text)
                        {
                            case "/start":
                            {
                                _startUpdate.Start(type, id, _botClient);
                                return;
                            }
                            case "Список дисциплин":
                            {
                                _disciplineUpdate.DisciplineUpdateAsync(type, id, _botClient);
                                return;
                            }
                            case "Список студентов":
                            {
                                _studentUpdate.StudentUpdateAsync(type, id, _botClient);
                                return;
                            }
                        }

                        return;
                    }
                }

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
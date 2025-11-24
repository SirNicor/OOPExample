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
public interface IStartBot
{
    public Task ListenForMessagesAsync();

    public Task HandleUpdateAsync
        (ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    
    public Task HandlePollingErrorAsync
        (ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
}
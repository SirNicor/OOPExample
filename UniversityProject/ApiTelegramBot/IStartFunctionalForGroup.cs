using UCore;

namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public interface IStartFunctionalForGroup
{
    public Task Functional(ChatId id, string messageText, ITelegramBotClient botClient, 
        ChatType type, UserStateRegistration userStateRegistration);
}
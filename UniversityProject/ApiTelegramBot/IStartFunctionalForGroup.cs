namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public interface IStartFunctionalForGroup
{
    public void Functional(ChatId id, string messageText, TelegramBotClient botClient);
}
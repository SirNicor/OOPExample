namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public interface IStartUpdate
{
    public void Start(ChatType type, ChatId id, TelegramBotClient botClient);
}
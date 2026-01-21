namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Repository;
public interface IStudentUpdate
{
    public void StudentUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId);
}
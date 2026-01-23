namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Repository;
public interface IStudentUpdate
{
    public Task StudentUpdateAsync(ChatId id, ITelegramBotClient botClient, long dirId, string type);
}
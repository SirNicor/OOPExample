namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Repository;
public interface IDisciplineUpdate
{
    public Task DisciplineUpdateAsync(ChatId id, ITelegramBotClient botClient, long dirId, string type);
}
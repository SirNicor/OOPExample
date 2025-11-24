namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Repository;
public interface IDisciplineUpdate
{
    public void DisciplineUpdateAsync(ChatType type, ChatId id, TelegramBotClient botClient);
}
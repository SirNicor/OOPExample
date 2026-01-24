using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
namespace ApiTelegramBot;
public interface IInitializedClass
{
    public Task Initialize(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg);
}
using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public interface IInitializedClass
{
    public void Initialize(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg);
}
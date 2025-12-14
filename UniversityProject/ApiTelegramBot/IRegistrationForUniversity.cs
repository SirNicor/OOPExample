using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public interface IRegistrationForUniversity
{
    public void Registration(long chatId, ITelegramBotClient botClient, string messageText,UserStateRegistration userStateRegistration);
}
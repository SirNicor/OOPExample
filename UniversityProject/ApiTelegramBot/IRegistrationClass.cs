namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UCore;
public interface IRegistrationClass
{
    public Task Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg);
}
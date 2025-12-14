namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
public interface IRegistrationClass
{
    public void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg);
}
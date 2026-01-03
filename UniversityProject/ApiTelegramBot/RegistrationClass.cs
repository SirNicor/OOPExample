using Telegram.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UCore;
namespace ApiTelegramBot;

public class RegistrationClass : IRegistrationClass
{
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText.ToLower() == "/registration")
        {
            userStateReg.IncrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свой ВУЗ: ");
        }
    }
}
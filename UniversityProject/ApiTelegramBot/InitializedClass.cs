using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;

public class InitializedClass : IInitializedClass
{
    private IDirectionRepository _directionRepository;
    public InitializedClass(IDirectionRepository directionRepository)
    {
        _directionRepository = directionRepository;
    }
    public async void Initialize(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText.ToLower() == "/start")
        {
            if (_directionRepository.AuthorizationVerification(userStateReg._chatId))
            {
                userStateReg.SetUserStateAsync(chatId, UserStateRegEnum.fullRegistration);
            }
            else
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/Registration"), }
                    }){ResizeKeyboard = true};
                await botClient.SendMessage(chatId, "Вы не зарегистрированы. Пройдите регистрацию", replyMarkup: replyKeyboard);
                userStateReg.IncrementUserStateAsync();
            }
        }
    }
}
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
    private IUserStateRepository _userStateRepository;
    public InitializedClass(IDirectionRepository directionRepository, IUserStateRepository userStateRepository)
    {
        _directionRepository = directionRepository;
        _userStateRepository = userStateRepository;
    }
    public async void Initialize(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText.ToLower() == "/start")
        {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/Registration"), }
                    }){ResizeKeyboard = true};
                await botClient.SendMessage(chatId, "Вы не зарегистрированы. Пройдите регистрацию", replyMarkup: replyKeyboard);
                userStateReg.IncrementUserStateAsync();
                _userStateRepository.Update(userStateReg);
        }
    }
}
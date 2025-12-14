using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForUniversity : IRegistrationForUniversity
{
    private IUniversityRepository _repository;
    public RegistrationForUniversity(IUniversityRepository repository)
    {
        _repository = repository;
    }
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText,UserStateRegistration userStateRegistration)
    {
        var id = _repository.CheckNameInUniversity(messageText);
        if (id != null)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[] { ("/RegistrationForUniversity"), }
                }){ResizeKeyboard = true};
            await botClient.SendMessage(chatId, "Такой университет существует, далее введите ваш факультет: ", replyMarkup: replyKeyboard);
            userStateRegistration.UniversityId = (long)id;
            userStateRegistration.IncrementUserStateAsync();
        }
        else
        {
            await botClient.SendMessage(chatId, "Такого университета нет. Введите снова");
        }
    }
}
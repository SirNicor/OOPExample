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
    private IUserStateTelegramRepository _userStateTelegramRepository;
    public RegistrationForUniversity(IUniversityRepository repository, IUserStateTelegramRepository userStateTelegramRepository )
    {
        _repository = repository;
        _userStateTelegramRepository = userStateTelegramRepository;
    }
    public async Task Registration(long chatId, ITelegramBotClient botClient, string messageText,UserStateRegistration userStateRegistration)
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
        _userStateTelegramRepository.Update(userStateRegistration);
    }
}
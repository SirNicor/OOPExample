using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForFaculty : IRegistrationForFaculty
{
    private IFacultyRepository _repository;
    public RegistrationForFaculty(IFacultyRepository repository)
    {
        _repository = repository;
    }
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText.ToLower() == "/registrationforuniversity")
        {
            userStateReg.DecrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свой ВУЗ: ");
        }
        else
        {
            var id = _repository.CheckNameFaculty(messageText, userStateReg.UniversityId);
            if (id != null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/RegistrationForFaculty"), }
                    }){ResizeKeyboard = true};
                await botClient.SendMessage(chatId, "Такой факультет существует, далее введите вашу кафедру: ", replyMarkup: replyKeyboard);
                userStateReg.FacultyId = (long)id;
                userStateReg.IncrementUserStateAsync();
            }   
            else
            {
                await botClient.SendMessage(chatId, "Такого факультета нет. Введите снова");
            }
        }
    }
}
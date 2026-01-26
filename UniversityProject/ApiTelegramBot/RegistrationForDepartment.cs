using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForDepartment : IRegistrationForDepartment
{
    private IDepartmentRepository _repository;
    private IUserStateTelegramRepository _userStateTelegramRepository;
    public RegistrationForDepartment(IDepartmentRepository repository, IUserStateTelegramRepository userStateTelegramRepository)
    {
        _repository = repository;
        _userStateTelegramRepository = userStateTelegramRepository;
    }
    public async Task Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText == "/RegistrationForFaculty")
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[] { ("/RegistrationForUniversity"), }
                }){ResizeKeyboard = true};
            userStateReg.DecrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свой факультет: ", replyMarkup: replyKeyboard);
        }
        else
        {
            var id = _repository.CheckNameDepartment(messageText, (long)userStateReg.FacultyId);
            if (id != null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/RegistrationForDepartment"), }
                    }){ResizeKeyboard = true};
                await botClient.SendMessage(chatId, "Такая кафедра существует, далее введите вашу группу: ", replyMarkup: replyKeyboard);
                userStateReg.DepartmentId = (long)id;
                userStateReg.IncrementUserStateAsync();
            }
            else
            {
                botClient.SendMessage(chatId, "Такой кафедры нет. Введите снова");
            }
        }
        _userStateTelegramRepository.Update(userStateReg);
    }
}
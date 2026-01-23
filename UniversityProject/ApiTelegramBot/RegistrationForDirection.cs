using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForDirection : IRegistrationForDirection
{
    private IDirectionRepository _repository;
    private IUserStateRepository _userStateRepository;
    public RegistrationForDirection(IDirectionRepository repository, IUserStateRepository userStateRepository)
    {
        _repository = repository;
        _userStateRepository = userStateRepository;
    }
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText == "/RegistrationForDepartment")
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[] { ("/RegistrationForFaculty"), }
                }){ResizeKeyboard = true};
            userStateReg.DecrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свою кафедру: ", replyMarkup: replyKeyboard);
        }
        else
        {
            var id = _repository.CheckNameDirection(messageText, (long)userStateReg.DepartmentId);
            if (id != null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/RegistrationForDirection"), }
                    }){ResizeKeyboard = true};
                await botClient.SendMessage(chatId, "Такая группа существует, далее введите вашу фамилию: ", replyMarkup: replyKeyboard);
                userStateReg.DirectionId = (long)id;
                userStateReg.IncrementUserStateAsync();
            }
            else
            {
                await botClient.SendMessage(chatId, "Такой группы нет. Введите снова");
            }
        }
        _userStateRepository.Update(userStateReg);
    }
}
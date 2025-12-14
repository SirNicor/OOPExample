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
    public RegistrationForDirection(IDirectionRepository repository)
    {
        _repository = repository;
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
            var id = _repository.CheckNameDirection(messageText, userStateReg.DepartmentId);
            if (id != null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[] { ("/RegistrationForDirection"), }
                    }){ResizeKeyboard = true};
                userStateReg.DecrementUserStateAsync();
                await botClient.SendMessage(chatId, "Такая группа существует, далее введите вашу фамилию: ", replyMarkup: replyKeyboard);
                userStateReg.DirectionId = (long)id;
                userStateReg.IncrementUserStateAsync();
            }
            else
            {
                await botClient.SendMessage(chatId, "Такой группы нет. Введите снова");
            }
        }
    }
}
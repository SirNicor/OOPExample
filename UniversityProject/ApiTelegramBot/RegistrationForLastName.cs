using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForLastName : IRegistrationForLastName
{
    private IStudentRepository _repository;
    public RegistrationForLastName(IStudentRepository repository)
    {
        _repository = repository;
    }
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        if (messageText == "/RegistrationForDirection")
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[] { ("/RegistrationForDepartment"), }
                }){ResizeKeyboard = true};
            userStateReg.DecrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свою группу: ", replyMarkup:replyKeyboard);
        }
        else
        {
            userStateReg.StudentLastName = messageText;
            userStateReg.IncrementUserStateAsync();
            await botClient.SendMessage(chatId, "Введите свое имя: "); 
        } 
    }
}
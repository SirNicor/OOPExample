using Telegram.Bot;

namespace ApiTelegramBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class RegistrationForFirstName : IRegistrationForFirstName
{
    private IStudentRepository _repository;
    private IDirectionRepository _directionRepository;
    public RegistrationForFirstName(IStudentRepository repository, IDirectionRepository directionRepository)
    {
        _repository = repository;
        _directionRepository = directionRepository;
    }
    public async void Registration(long chatId, ITelegramBotClient botClient, string messageText, UserStateRegistration userStateReg)
    {
        var id = _repository.CheckName(messageText, userStateReg.StudentLastName);
        if (id != null)
        {
            userStateReg.StudentFirstName = messageText;
            userStateReg.StudentId = (long)id;
            if (_directionRepository.CheckStudent(userStateReg.StudentId))
            {
                userStateReg.IncrementUserStateAsync();
                await botClient.SendMessage(chatId, "Регистрация успешно завершена");
            }
            else
            {
                await botClient.SendMessage(chatId, "Студент с таким сочетанием имени и фамилии не принадлежит к названной группе. Введите снова фамилию");
                userStateReg.DecrementUserStateAsync();
            }
        }
        else
        {
            botClient.SendMessage(chatId, "Такого сочетания имени и фамилии нет. Введите снова фамилию");
            userStateReg.DecrementUserStateAsync();
        }
    }   
}
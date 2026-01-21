namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Repository;
using UCore;
public class StartFunctionalForGroup : IStartFunctionalForGroup
{
    private IDisciplineUpdate _disSend;
    private IStudentUpdate _studentUpdate;
    private IScheduleUpdate _scheduleUpdate;
    private IDirectionRepository _directionRepository;
    private IUserStateRepository _userStateRepository;  
    private long dirId;
    public StartFunctionalForGroup(IStudentRepository studentRepository, IDirectionRepository directionRepository, 
        IDisciplineUpdate disSend, IStudentUpdate studentUpdate, IUserStateRepository userStateRepository,
        IScheduleUpdate scheduleUpdate)
    {
        _studentUpdate = studentUpdate;
        _disSend = disSend;
        _scheduleUpdate = scheduleUpdate;
        _directionRepository = directionRepository;
        _userStateRepository = userStateRepository;
    }
    public async void Functional(ChatId id, string messageText, TelegramBotClient botClient, ChatType type)
    {
        if(type == ChatType.Private)
        {
            dirId = (long)_userStateRepository.Get((long)id.Identifier).DirectionId;
        }
        else if(type == ChatType.Group)
        {
            dirId = _directionRepository.AuthorizationVerification((long)id.Identifier);
        }
        switch (messageText)
            {
                case "/start":
                case "/Start":
                    {
                        var replyKeyboard = new ReplyKeyboardMarkup(
                            new List<KeyboardButton[]>()
                            {
                        new KeyboardButton[] { ("Список дисциплин"), "Список студентов", },
                        new KeyboardButton[] { "Расписание" },
                            })
                        {
                            ResizeKeyboard = true,
                        };

                        await botClient.SendMessage(
                            id,
                            "Функционал:",
                            replyMarkup: replyKeyboard);
                        return;
                    }
                case "Список дисциплин":
                    {
                        _disSend.DisciplineUpdateAsync(id, botClient, dirId);
                        return;
                    }
                case "Список студентов":
                    {
                        _studentUpdate.StudentUpdateAsync(id, botClient, dirId);
                        return;
                    }
                case "Расписание":
                    {
                        _scheduleUpdate.ScheduleUpdateAsync(id, botClient, dirId);
                        return;
                    }
            }
    }
        
}
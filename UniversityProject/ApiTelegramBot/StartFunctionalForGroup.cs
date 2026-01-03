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
    public StartFunctionalForGroup(IStudentRepository studentRepository, IDisciplineUpdate disSend, IStudentUpdate studentUpdate)
    {
        _studentUpdate = studentUpdate;
        _disSend = disSend;
    }
    public async void Functional(ChatId id, string messageText, TelegramBotClient botClient)
    {
        switch (messageText)
        {
            case "/start": case "/Start":
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
                _disSend.DisciplineUpdateAsync(id, botClient);
                return;
            }
            case "Список студентов":
            {
                _studentUpdate.StudentUpdateAsync(id, botClient);
                return;
            }
        }
    }
        
}
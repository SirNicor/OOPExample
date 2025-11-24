namespace ApiTelegramBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
public class StartUpdate : IStartUpdate
{
    public async void Start(ChatType type,ChatId id, TelegramBotClient botClient)
    {
        if (type == ChatType.Group)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[] { ("Список дисциплин"),"Список студентов", },
                    new KeyboardButton[] { "Расписание" },
                })
            {
                ResizeKeyboard = true,
            };

            await botClient.SendMessage(
                id,
                "Функционал:",
                replyMarkup: replyKeyboard);
        }
        else if (type == ChatType.Private)
        {
            await botClient.SendMessage(id, "Введите свою фамилию в именительном падеже: ");
                                                
        }
    }
}
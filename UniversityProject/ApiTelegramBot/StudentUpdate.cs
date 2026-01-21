using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.Json;
using System.Text;
using Repository;
namespace ApiTelegramBot;

public class StudentUpdate : IStudentUpdate
{
    private IDirectionRepository _directionRepository;
    private ICreateMessageClass _createMessageClass;
    public StudentUpdate(IDirectionRepository directionRepository, ICreateMessageClass createMessageClass)
    {
        _directionRepository = directionRepository;
        _createMessageClass = createMessageClass;
    }
    public async void StudentUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId)
    {
        var htmlOfstudents = _createMessageClass.StudentMessage(_directionRepository.Get(dirId).Students);
        foreach (var student in htmlOfstudents)
        {
            await botClient.SendMessage(id, student, ParseMode.Html);
        }
    }
}
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
    public StudentUpdate(IDirectionRepository directionRepository)
    {
        _directionRepository = directionRepository;
    }
    public async void StudentUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId)
    {
        var students = JsonSerializer.Serialize(_directionRepository.Get(dirId).Students);
        var studentsMessage = students.SplitMessage();
        foreach (var student in studentsMessage)
        {
            await botClient.SendMessage(id, student);
        }
    }
}
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
    public async void StudentUpdateAsync(ChatType type, ChatId id, TelegramBotClient botClient)
    {
        var students = JsonSerializer.Serialize(_directionRepository.Get(1).Students);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(students));
        await botClient.SendDocument(id, InputFile.FromStream(stream, "json"), parseMode: ParseMode.Html);
        return;
    }
}
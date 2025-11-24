using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.Json;
using System.Text;
using Repository;
namespace ApiTelegramBot;

public class DisciplineUpdate : IDisciplineUpdate
{
    private IDirectionRepository _dirRepository;
    public DisciplineUpdate(IDirectionRepository dirRepository)
    {
        _dirRepository = dirRepository;
    }
    public async void DisciplineUpdateAsync(ChatType type, ChatId id, 
        TelegramBotClient botClient)
    {
        var disciplinesJson = JsonSerializer.Serialize(_dirRepository.Get(1).Disciplines);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(disciplinesJson));
        await botClient.SendDocument(id, InputFile.FromStream(stream, "json"), parseMode: ParseMode.Html);
        return;
    }
}
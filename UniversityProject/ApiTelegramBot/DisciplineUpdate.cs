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
    private ICreateMessageClass _createMessageClass;
    public DisciplineUpdate(IDirectionRepository dirRepository, ICreateMessageClass createMessageClass)
    {
        _dirRepository = dirRepository;
        _createMessageClass = createMessageClass;
    }
    public async void DisciplineUpdateAsync(ChatId id, 
        TelegramBotClient botClient, long dirId)
    {
        var htmlOfDisciplines = _createMessageClass.DisciplineMessage(_dirRepository.Get(dirId).Disciplines);
        foreach (var discipline in htmlOfDisciplines) 
        {
            await botClient.SendMessage(id, discipline, ParseMode.Html);
        }
    }
}
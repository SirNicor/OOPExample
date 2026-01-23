using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text.Json;
using System.Text;
using Repository;
using SelectPdf;
using Logger;
using Microsoft.Extensions.Logging;

namespace ApiTelegramBot;

public class StudentUpdate : IStudentUpdate
{
    private IDirectionRepository _directionRepository;
    private ICreateMessageClass _createMessageClass;
    private MyLogger _logger;
    public StudentUpdate(IDirectionRepository directionRepository, ICreateMessageClass createMessageClass, MyLogger logger)
    {
        _directionRepository = directionRepository;
        _createMessageClass = createMessageClass;
        _logger = logger;
    }
    public async Task StudentUpdateAsync(ChatId id, ITelegramBotClient botClient, long dirId, string type)
    {
        var htmlOfstudents = _createMessageClass.StudentMessage(_directionRepository.Get(dirId).Students);
        if (type == "text")
        {
            foreach (var student in htmlOfstudents)
            {
                await botClient.SendMessage(id, student, ParseMode.Html);
            }
        }
        else
        {
            try
            {
                var allHtml = htmlOfstudents.Aggregate((x, y) => $"{x}\n{y}");
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(allHtml);
                var result = new MemoryStream();
                doc.Save(result);
                result.Position = 0;
                await botClient.SendDocument(id, InputFile.FromStream(result, "Студенты.pdf"));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }
    }
}
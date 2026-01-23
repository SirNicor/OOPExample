using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Text.Json;
using Telegram.Bot.Types;
using Repository;
using Telegram.Bot.Types.Enums;
using SelectPdf;
using Logger;

namespace ApiTelegramBot
{
    public class ScheduleUpdate : IScheduleUpdate
    {

        private IScheduleRepository _scheduleRepository;
        private ICreateMessageClass _createMessageClass;
        private MyLogger _logger;
        public ScheduleUpdate(IScheduleRepository scheduleRepository, ICreateMessageClass createMessageClass, MyLogger myLogger)
        {
            _scheduleRepository = scheduleRepository;
            _createMessageClass = createMessageClass;
            _logger = myLogger;
        }
        public async Task ScheduleUpdateAsync(ChatId id, ITelegramBotClient botClient, long dirId, string type)
        {
            var htmlOfSchedule = _createMessageClass.ScheduleMessage(_scheduleRepository.ReturnListForDirectionId(dirId));
            if (type == "text")
            {
                foreach (var schedule in htmlOfSchedule)
                {
                    await botClient.SendMessage(id, schedule, ParseMode.Html);
                }
            }
            else
            {
                try
                {
                    var allHtml = htmlOfSchedule.Aggregate((x, y) => $"{x}\n{y}");
                    HtmlToPdf converter = new HtmlToPdf();
                    PdfDocument doc = converter.ConvertHtmlString(allHtml);
                    var result = new MemoryStream();
                    doc.Save(result);
                    result.Position = 0;
                    await botClient.SendDocument(id, InputFile.FromStream(result, "Расписание.pdf"));
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    throw;
                }
            }
        }
    }
}

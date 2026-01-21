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

namespace ApiTelegramBot
{
    public class ScheduleUpdate : IScheduleUpdate
    {

        private IScheduleRepository _scheduleRepository;
        private ICreateMessageClass _createMessageClass;
        public ScheduleUpdate(IScheduleRepository scheduleRepository, ICreateMessageClass createMessageClass)
        {
            _scheduleRepository = scheduleRepository;
            _createMessageClass = createMessageClass;
        }
        public async void ScheduleUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId)
        {
            var htmlOfSchedule = _createMessageClass.ScheduleMessage(_scheduleRepository.ReturnListForDirectionId(dirId));
            foreach (var schedule in htmlOfSchedule)
            {
                await botClient.SendMessage(id, schedule, ParseMode.Html);
            }
        }
    }
}

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

namespace ApiTelegramBot
{
    public class ScheduleUpdate : IScheduleUpdate
    {

        private IScheduleRepository _scheduleRepository;
        public ScheduleUpdate(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        public async void ScheduleUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId)
        {
            var scheduleJson = JsonSerializer.Serialize(_scheduleRepository.ReturnListForDirectionId(dirId));
            var scheduleArray = scheduleJson.SplitMessage();
            foreach (var schedule in scheduleArray)
            {
                await botClient.SendMessage(id, schedule);
            }
        }
    }
}

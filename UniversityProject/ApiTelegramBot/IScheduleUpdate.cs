using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Repository;

namespace ApiTelegramBot
{
    public interface IScheduleUpdate
    {
        public void ScheduleUpdateAsync(ChatId id, TelegramBotClient botClient, long dirId);
    }
}

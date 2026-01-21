using UCore;
using Microsoft.AspNetCore.Html;
using System.Text;
namespace ApiTelegramBot;
public interface ICreateMessageClass
{
    public List<string> StudentMessage(List<Student> students);
    public List<string> ScheduleMessage(List<Schedule> schedules);
    public List<string> DisciplineMessage(List<Discipline> disciplines);
}
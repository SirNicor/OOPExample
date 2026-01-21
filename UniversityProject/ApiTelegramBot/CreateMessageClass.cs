using System.Text;
using UCore;

namespace ApiTelegramBot;
using Microsoft.AspNetCore.Html;
public class CreateMessageClass : ICreateMessageClass
{
    const int _maxLength = 400;
    
    public List<string> StudentMessage(List<Student> students)
    {
        List<string> _htmlStrings = new List<string>();
        StringBuilder _stringBuilder;
        int _length;
        _stringBuilder = new StringBuilder("<b>Список студентов:</b>\n");
        foreach (var student in students)
        {
            _stringBuilder.Append("------\n");
            _stringBuilder.Append($"<i>Полное имя:</i> {student.Passport.FirstName} {student.Passport.LastName}\n");
            _stringBuilder.Append(
                $"<i>Курс:</i> {student.Course}, <i>балл:</i> {student.TotalScore}, <i>пропуски:</i> {student.CreditScores}\n");
            _length = _stringBuilder.Length - 28;
            if (_length > _maxLength*0.9)
            {
                _htmlStrings.Add(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }
        }
        if (_stringBuilder.Length != 0)
        {
            _htmlStrings.Add(_stringBuilder.ToString());
        }
        return _htmlStrings;
    }

    public List<String> ScheduleMessage(List<Schedule> schedules)
    {
        List<string> _htmlStrings = new List<string>();
        StringBuilder _stringBuilder;
        int _length;
        _stringBuilder = new StringBuilder("<b>Расписание:</b>\n");
        foreach (var schedule in schedules)
        {
            _stringBuilder.Append("------\n");
            _stringBuilder.Append($"<i>Название дисциплины:</i> {schedule.Discipline.NameDiscipline}\n");
            _stringBuilder.Append($"<i>Преподаватель:</i> {schedule.Teacher.Passport.FirstName} {schedule.Teacher.Passport.LastName}\n");
            _stringBuilder.Append($"<i>Время:</i> {schedule.DataWeek}, {schedule.StartCouple} - {schedule.EndCouple}\n");
            _length = _stringBuilder.Length - 21;
            if (_length > _maxLength*0.8)
            {
                _htmlStrings.Add(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }
        }
        if (_stringBuilder.Length != 0)
        {
            _htmlStrings.Add(_stringBuilder.ToString());
        }
        return _htmlStrings;
    }

    public List<string> DisciplineMessage(List<Discipline> disciplines)
    {
        List<string> _htmlStrings = new List<string>();
        StringBuilder _stringBuilder;
        int _length;
        _stringBuilder = new StringBuilder("<b>Список дисциплин:</b>\n");
        foreach (var discipline in disciplines)
        {
            _stringBuilder.Append("------\n");
            _stringBuilder.Append($"<i>Название:</i> {discipline.NameDiscipline}\n");
            _length = _stringBuilder.Length - 7;
            if (_length > _maxLength*0.8)
            {
                _htmlStrings.Add(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }
        }
        if (_stringBuilder.Length != 0)
        {
            _htmlStrings.Add(_stringBuilder.ToString());
        }
        return _htmlStrings;
    }
}
namespace ApiTelegramBot;
using System.Text;
using UCore;
using SelectPdf;
public class CreateFileClass
{
    public Stream StudentMessage(List<Student> students)
    {
        StringBuilder stringBuilder;
        stringBuilder = new StringBuilder("<h1>Список студентов:</h1>");
        foreach (var student in students)
        {
            stringBuilder.Append("<p>------</p>");
            stringBuilder.Append($"<div><i>Полное имя:</i> {student.Passport.FirstName} {student.Passport.LastName}</div>");
            stringBuilder.Append(
                $"<div><i>Курс:</i> {student.Course}, " +
                $"<i>балл:</i> {student.TotalScore}, <i>пропуски:</i> {student.CreditScores}</div>");
        }
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = converter.ConvertHtmlString(stringBuilder.ToString());
        var result = new MemoryStream();
        doc.Save(result);
        result.Position = 0;
        return result;
    }

    public Stream ScheduleMessage(List<Schedule> schedules)
    {
        StringBuilder stringBuilder;
        stringBuilder = new StringBuilder("<h1>Расписание:</h1>");
        foreach (var schedule in schedules)
        {
            stringBuilder.Append("<p>------<p>");
            stringBuilder.Append($"<div><i>Название дисциплины:</i> {schedule.Discipline.NameDiscipline}</div>");
            stringBuilder.Append($"<div><i>Преподаватель:</i> {schedule.Teacher.Passport.FirstName} {schedule.Teacher.Passport.LastName}</div>");
            stringBuilder.Append($"<div><i>Время:</i> {schedule.DataWeek}, {schedule.StartCouple} - {schedule.EndCouple}</div>");
        }
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = converter.ConvertHtmlString(stringBuilder.ToString());
        var result = new MemoryStream();
        doc.Save(result);
        result.Position = 0;
        return result;
    }

    public Stream DisciplineMessage(List<Discipline> disciplines)
    {
        StringBuilder stringBuilder;
        stringBuilder = new StringBuilder("<h1>Список дисциплин:</h1>");
        foreach (var discipline in disciplines)
        {
            stringBuilder.Append("<p>------</p>");
            stringBuilder.Append($"<div><i>Название:</i> {discipline.NameDiscipline}</div>");
        }
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = converter.ConvertHtmlString(stringBuilder.ToString());
        var result = new MemoryStream();
        doc.Save(result);
        result.Position = 0;
        return result;
    }
}
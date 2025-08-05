namespace UCore;
using Logger;
public class Applicant:Person
{
    public Applicant(Direction[] directionsOfAdmission, int scores)
    {
        _directionsOfAdmission = directionsOfAdmission;
        _scores = scores;
    }
    private Direction[] _directionsOfAdmission;
    private int _scores;
    public override void PrintDerivedClass(Logger logger)
    {
        string message = "Все направления для поступления: ";
        foreach (Direction str in _directionsOfAdmission)
        {
            message += str.ReturnGroupCipher() + ", ";
        }

        message += Environment.NewLine + $"Баллы за экзамен: {_scores}";
        logger.Info(message);
    }
}
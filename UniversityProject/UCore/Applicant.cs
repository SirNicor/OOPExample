namespace University.UCore;

public class Applicant:Person
{
    public Applicant(Direction[] directionsOfAdmission, int scores)
    {
        _directionsOfAdmission = directionsOfAdmission;
        _scores = scores;
    }
    private Direction[] _directionsOfAdmission;
    private int _scores;
}
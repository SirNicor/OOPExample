namespace University;

public static class CheckMethods
{
    public static int CheckDegress(int course, DegreesStudy degrees)
    {
        if (course > (int)degrees)
        {
            return (int)degrees;
        }
        if (course < 1)
        {
            return 1;
        }
        return course;
    }
}
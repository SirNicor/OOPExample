namespace University.UCore;

public enum DegreesStudy
{
    bachelor = 4,
    specialization = 5,
    master = 6,
    doctoral = 10,
}

public enum IdMillitary
{   
    DidNotServe = 0, 
    PostponementHealth = 1,
    PostponementUniversityB = DegreesStudy.bachelor,
    PostponementUniversityS = DegreesStudy.specialization,
    ExcludedFromTheStock,
    InStock,
}
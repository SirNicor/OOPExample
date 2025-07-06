using System;
using System.Runtime.InteropServices;

namespace University
{
    class Program
    {
        static void Main()
        {
            Discipline AppliedComputerScience = new Discipline(
                "FITM", new Administrator(), new Administrator(), 
                "IMST", new Administrator(), "AppliedComputerScience");
            Console.WriteLine(AppliedComputerScience.ReturnGroupCipher());
        }
    }
}

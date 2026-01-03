using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiTelegramBot;

public static class SplitClass
{
    public static List<string> SplitMessage(this string s, int maxLength = 4096)
    {
        if (string.IsNullOrEmpty(s) || s.Length <= maxLength)
        {
            return new List<string>() { s };
        }
        var result = new List<string>();
        while (s.Length > maxLength)
        {
            int id = FindSplit(s, maxLength);
            result.Add(s.Substring(0, id).Trim());
            s = s.Substring(id+1).Trim();
        }
        if (!string.IsNullOrEmpty(s))
        {
            result.Add(s);
        }
        return result;
    }

    private static int FindSplit(string s, int maxLength)
    {
        int[] SplitIndex =
        {
            s.LastIndexOf("\n\n", Math.Min(maxLength, s.Length - 1)),
            s.LastIndexOf("\n", Math.Min(maxLength, s.Length - 1)),
        };
        foreach (int ind in SplitIndex)
        {
            if (ind > maxLength * 0.8)
            {
                return ind + 1;
            }
        }

        return maxLength;
    }
}
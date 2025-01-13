
using System.Text.RegularExpressions;

int n = int.Parse(Console.ReadLine());


string pattern = @"([*@])([A-Z][a-z]{2,})\1: \[([A-Za-z])\]\|\[([A-Za-z])\]\|\[([A-Za-z])\]\|$";

for (int i = 0; i < n; i++)
{
    string input = Console.ReadLine();
    Match match = Regex.Match(input, pattern);

    if (match.Success)
    {
        string tag = match.Groups[2].Value;
        int firstAscii = match.Groups[3].Value[0];
        int secondAscii = match.Groups[4].Value[0];
        int thirdAscii = match.Groups[5].Value[0];

        Console.WriteLine($"{tag}: {firstAscii} {secondAscii} {thirdAscii}");
    }
    else
    {
        Console.WriteLine("Valid message not found!");
    }
}
   
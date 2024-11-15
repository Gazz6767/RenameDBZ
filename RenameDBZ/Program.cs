using RenameDBZ;
using System.Text.RegularExpressions;

Console.WriteLine("Paste Full Path To The Root Folder (where the season folders are: ");
string folderPath = Console.ReadLine() ?? String.Empty;

if (!Directory.Exists(folderPath))
{
    Console.WriteLine("The Folder Path Does Not Exist!");
    Console.ReadLine();
    return;
}

Console.WriteLine("Collecting file names...");
Stuff functions = new();
IEnumerable<string> fileNames = functions.GetFilesInDirectory("E:\\Videos\\Dragon Ball Z", true);

Console.WriteLine("Changing file names...");
string EposideRegexPattern = @"E0?[1-9][0-9]?[0-9]?[0-9]?";
int seasonNumber = 1;
int episodeNumber = 1;
bool isNewSeason = true;

foreach (string fileName in fileNames)
{
    if (!fileName.Contains("RenameDBZ.exe"))
    {
        if (!fileName.Contains($"Season {seasonNumber}"))
        {
            seasonNumber++;
            isNewSeason = true;
        }

        if (isNewSeason)
        {
            episodeNumber = 1;
        }

        string newName = fileName.Replace(" (720p - TRI Audio)", String.Empty) ?? fileName;
        newName = fileName.Replace(" (720p BluRay TRI Audio)", String.Empty) ?? fileName;
        newName = newName.Replace("- ", String.Empty);
        newName = newName.Replace("S00", "S0");
        newName = newName.Replace(" E0", "E0");
        newName = newName.Replace(" E1", "E1");
        newName = newName.Replace(" E2", "E2");
        newName = newName.Replace(" E3", "E3");
        Match episodeMatch = Regex.Match(newName, EposideRegexPattern, RegexOptions.IgnoreCase);
        if (episodeMatch.Success)
        {
            if (episodeNumber > 9)
            {
                newName = Regex.Replace(newName, EposideRegexPattern, $"E{episodeNumber}");
            }
            else
            {
                newName = Regex.Replace(newName, EposideRegexPattern, $"E0{episodeNumber}");
            }

        }
        File.Move(fileName, newName);
        isNewSeason = false;
        episodeNumber++;
    }
}

Console.WriteLine("Finished!");
Console.ReadLine();

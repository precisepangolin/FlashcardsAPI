using FlashcardsAPI;
using FlashcardsAPI.Infrastructure;

DataService service= new DataService();
IEnumerable<string> folderNames = service.UserFolders.Select(f => f.Name);
IEnumerable<int> folderLengths = service.UserFolders.Select(f => f.TotalFlashcards);
foreach (string folderName in folderNames)
{
    Console.WriteLine(folderName);
}

foreach (int folderLength in folderLengths)
{
    Console.WriteLine(folderLength);
}
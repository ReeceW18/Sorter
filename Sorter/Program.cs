/*
    Sort Helper CLI
    Helps user sort a subjective list
*/

namespace Sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            // variable declarations
            string baseFolder = "/home/reece/Documents/Programming/C#/Sorter/ProgramFiles"; // 

            // folders
            string testListsFolder = Path.Combine(baseFolder, "testLists");
            string privateListsFolder = Path.Combine(baseFolder, "privateLists");
            string sortedListsFolder = Path.Combine(baseFolder, "sortedLists");
            string savesFolder = Path.Combine(baseFolder, "saves");

            // bools
            bool isRunning = true, isSaveFile = false, isSorted = false;

            // strings
            string[] files, fileNames;
            List<string> workingList;
            string selectedFile, fileName;

            // ints
            int fileIndex;

            // main loop
            // better way than having everything nested in while?
            while (isRunning)
            {
                // ask whether to load from save or load new file
                Console.WriteLine("Would you like to load from save (0), or load from file (1)?");
                isSaveFile = Utils.UserIntInput(0, 1) == 0;

                // different behavior depending on whether loading from save or not
                // could probably be simplified to have less nested behavior
                if (isSaveFile)
                {
                    // get user to select save file
                    files = Directory.GetFiles(savesFolder, "*.txt");
                    fileIndex = Utils.UserSelectFromList(files);
                    selectedFile = files[fileIndex];
                    fileName = Path.GetFileName(selectedFile).Replace(".txt", "");

                    // load save data
                    (workingList, var savedName, var savedComparisons, var savedPivots) = LoadSaveData(selectedFile);
                    Sorter sorter = new Sorter(workingList, savedName, savesFolder, savedComparisons, savedPivots);

                    // sort data
                    isSorted = sorter.UserSort();
                }
                else
                {
                    // get user to select file to load
                    files = Directory.GetFiles(privateListsFolder, "*.txt");
                    files = files.Concat(Directory.GetFiles(testListsFolder, "*.txt")).ToArray();
                    fileNames = files.Select(s => Path.GetFileName(s).Replace(".txt", "")).ToArray();
                    fileIndex = Utils.UserSelectFromList(fileNames);
                    selectedFile = files[fileIndex];
                    fileName = Path.GetFileName(selectedFile).Replace(".txt", "");


                    // load file
                    workingList = new List<string>(File.ReadAllLines(selectedFile));
                    Sorter sorter = new Sorter(workingList, Path.GetFileName(selectedFile), savesFolder);

                    // sort
                    isSorted = sorter.UserSort();
                }

                // if list was successfully sorted write out to file
                if (isSorted)
                {
                    Console.WriteLine("Sorted list: ");
                    foreach (var entry in workingList)
                    {
                        Console.WriteLine(entry);
                    }
                    Console.WriteLine("Writing to file");
                    Utils.WriteListToFile(workingList, sortedListsFolder, fileName);
                    Console.WriteLine("Done, returning to program start");
                }
            }


        }

        static (List<string>, string, List<ComparedStrings>, List<int>) LoadSaveData(string filePath)
        {
            // Load sorting information from save file, returns tuple with all data
            // set up variables
            List<string> savedList = [];
            string savedName = "unable to retrive name";
            List<ComparedStrings> savedComparisons = [];
            List<int> savedPivots = [];

            // read lines into list
            string[] lines = File.ReadAllLines(filePath);

            // pull out data into respective variables
            savedName = lines[0];
            savedComparisons = ParseComparisons(lines[1]);

            var pivotsAsString = lines[2].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            savedPivots = pivotsAsString.Select(int.Parse).ToList();

            for (int i = 3; i < lines.Length; i++)
            {
                savedList.Add(lines[i]);
            }

            return (savedList, savedName, savedComparisons, savedPivots);

        }

        static List<ComparedStrings> ParseComparisons(string comparisonsAsString)
        {
            // parses comparisons from a string and returns parsed list
            List<ComparedStrings> savedComparisons = [];
            string[] comparisonsStringList = comparisonsAsString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var comparisonAsString in comparisonsStringList)
            {
                string trimmed = comparisonAsString.Trim('(', ')');
                string[] values = trimmed.Split(',');
                savedComparisons.Add(new ComparedStrings(values[0], values[1]));
            }

            return savedComparisons;
        }

    }
}

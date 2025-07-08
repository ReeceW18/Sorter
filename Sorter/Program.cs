// See https://aka.ms/new-console-template for more information
string directory = "/home/reece/Documents/Programming/C#/Sorter/files"; // directory to store lists
string[] algortihms = { "Bubble Sort", "Selection Sort", "Insertion Sort", "Merge Sort", "Quick Sort" }; // sorting algorithms

// display files and ask/read input
Console.WriteLine("Files:");
string[] txtFiles = Directory.GetFiles(directory, "*.txt");
int number = 0;
foreach (string file in txtFiles)
{
    Console.WriteLine($"{number}: " + Path.GetFileName(file));
    number++;
}
Console.Write("Which file do you want to sort? (Enter the number): ");
int fileIndex;
while (!int.TryParse(Console.ReadLine(), out fileIndex) || fileIndex < 0 || fileIndex >= txtFiles.Length)
{
    Console.WriteLine("Invalid input. Please enter a valid number corresponding to the file you want to sort.");
}

// display sorting algorithms and ask/read input
Console.WriteLine("What sorting algorithm would you like to use?");
number = 0;
foreach (string algorithm in algortihms)
{
    Console.WriteLine($"{number}: " + algorithm);
    number++;
}
Console.Write("Enter a number: ");
int algorithmIndex;
while (!int.TryParse(Console.ReadLine(), out algorithmIndex) || algorithmIndex < 0 || algorithmIndex >= algortihms.Length)
{
    Console.WriteLine("Invalid input. Please enter a valid number corresponding to the sorting algorithm you want to use.");
}

Console.WriteLine($"You selected file: {Path.GetFileName(txtFiles[fileIndex])} with algorithm: {algortihms[algorithmIndex]}");

List<string> lines = new List<string>(File.ReadAllLines(txtFiles[fileIndex]));

// sort the lines based on the selected algorithm
if (algorithmIndex == 0) {
    Sorter.Sort(lines);
} else if (algorithmIndex == 1) {
    SelectionSorter.Sort(lines);
} 
else if (algorithmIndex == 2) {
    InsertionSorter.Sort(lines);
} 
else if (algorithmIndex == 3) {
    MergeSorter.Sort(lines);
} 
else if (algorithmIndex == 4) {
    QuickSorter.Sort(lines);
}

File.WriteAllLines(txtFiles[fileIndex], lines);

Console.WriteLine("Done");

class Sorter
{
    protected static bool IsGreater(string item1, string item2)
    {
        // is item1 greater than item2?
        Console.WriteLine("1: " + item1);
        Console.WriteLine("2: " + item2);
        Console.WriteLine("Which item is greater? (1/2)");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }
        return !(choice == 1);
    }

    protected static bool IsLess(string item1, string item2)
    {
        return !IsGreater(item1, item2);
    }
    public static void Sort(List<string> lines)
    {
        int n = lines.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (IsGreater(lines[j], lines[j + 1]))
                {
                    // swap lines[j] and lines[j + 1]
                    string temp = lines[j];
                    lines[j] = lines[j + 1];
                    lines[j + 1] = temp;
                }
            }
        }
    }

}

class BubbleSorter : Sorter
{
    public static void Sort(List<string> lines)
    {
        int n = lines.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (IsGreater(lines[j], lines[j + 1]))
                {
                    // swap lines[j] and lines[j + 1]
                    string temp = lines[j];
                    lines[j] = lines[j + 1];
                    lines[j + 1] = temp;
                }
            }
        }
    }
}
class SelectionSorter : Sorter {
    
}

class InsertionSorter : Sorter
{

}

class MergeSorter : Sorter
{
    public static void Sort(List<string> lines)
    {
        MergeSort(lines, 0, lines.Count - 1);
    }

    private static void MergeSort(List<string> lines, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            MergeSort(lines, left, mid);
            MergeSort(lines, mid + 1, right);

            Merge(lines, left, mid, right);
        }
    }

    private static void Merge(List<string> lines, int left, int mid, int right)
    {
        int sizeLeft = mid - left + 1;
        int sizeRight = right - mid;

        List<string> leftList = new List<string>();
        List<string> rightList = new List<string>();

        for (int i = 0; i < sizeLeft; i++)
        {
            leftList.Add(lines[left + i]);
        }
        for (int i = 0; i < sizeRight; i++)
        {
            rightList.Add(lines[mid + 1 + i]);
        }

        int leftIndex = 0, rightIndex = 0;
        int currIndex = left;

        while (leftIndex < sizeLeft && rightIndex < sizeRight)
        {
            if (IsLess(leftList[leftIndex], rightList[rightIndex]))
            {
                lines[currIndex] = leftList[leftIndex];
                leftIndex++;
            }
            else
            {
                lines[currIndex] = rightList[rightIndex];
                rightIndex++;
            }
            currIndex++;
        }

        while (leftIndex < sizeLeft)
        {
            lines[currIndex] = leftList[leftIndex];
            leftIndex++;
            currIndex++;
        }

        while (rightIndex < sizeRight)
        {
            lines[currIndex] = rightList[rightIndex];
            rightIndex++;
            currIndex++;
        }
    }
}

class QuickSorter : Sorter
{

}
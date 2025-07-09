// Sort Helper CLI

using System.Configuration.Assemblies;

string directory = "/home/reece/Documents/Programming/C#/Sorter/lists"; // directory to store lists
string[] algortihms = { "Quick Sort", "Merge Sort", "Bubble Sort" }; // sorting algorithms

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
/*if (algorithmIndex == 0) {
    Sorter.Sort(lines);
} else if (algorithmIndex == 1) {
    MergeSorter.Sort(lines);
} 
else (algorithmIndex == 2) {
    BubbleSorter.Sort(lines);
}*/
Sorter sorter;
switch (algorithmIndex) {
    case 0:
        sorter = new QuickSorter();
        break;
    case 1:
        sorter = new MergeSorter();
        break;
    case 2:
        sorter = new BubbleSorter();
        break;
    default:
        throw new Exception("invalid algorithm index");
}
sorter.Sort(lines);

File.WriteAllLines(directory + "/sorted/" + Path.GetFileName(txtFiles[fileIndex]).Replace(".txt", "_sorted.txt"), lines);

Console.WriteLine("done");

struct ItemComparison
{
    public string item1;
    public string item2;

    public ItemComparison(string one, string two)
    {
        item1 = one;
        item2 = two;
    }

    public bool has(string item)
    {
        return item == item1 || item == item2;
    }
}
public class Sorter
{
    private List<ItemComparison> items = new List<ItemComparison>();

    protected bool IsGreater(string item1, string item2)
    {
        // is item1 greater than item2?
        foreach (var comp in items)
        {
            if (comp.has(item1) && comp.has(item2))
            {
                if (comp.item1 == item1) return true;
                else return false;
            }
        }
        Console.WriteLine("1: " + item1);
        Console.WriteLine("2: " + item2);
        Console.WriteLine("Which item is greater? (1/2)");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }
        if (choice == 1)
        {
            StoreComparison(item1, item2);
        }
        else
        {
            StoreComparison(item2, item1);
        }

        return !(choice == 1);
    }

    protected bool IsLess(string item1, string item2)
    {
        return !IsGreater(item1, item2);
    }

    private void StoreComparison(string first, string second)
    {
        items.Add(new ItemComparison(first, second));
        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < items.Count; j++)
            {
                bool cont = true;
                if (j != i && items[j].item1 == items[i].item2)
                {
                    foreach (var comp in items)
                    {
                        if (comp.has(items[i].item1) && comp.has(items[j].item2)) cont = false;
                        if (!cont) break;
                    }
                    if (!cont) continue;
                    StoreComparison(items[i].item1, items[j].item2);
                }
            }
        }
    }
    public virtual void Sort(List<string> lines)
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

public class BubbleSorter : Sorter
{
    public override void Sort(List<string> lines)
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
public class MergeSorter : Sorter
{
    public override void Sort(List<string> lines)
    {
        MergeSort(lines, 0, lines.Count - 1);
    }

    private void MergeSort(List<string> lines, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            MergeSort(lines, left, mid);
            MergeSort(lines, mid + 1, right);

            Merge(lines, left, mid, right);
        }
    }

    private void Merge(List<string> lines, int left, int mid, int right)
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
    public override void Sort(List<string> lines)
    {
        quickSort(lines, 0, lines.Count - 1);
    }

    private void quickSort(List<string> lines, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = partition(lines, low, high);
            quickSort(lines, low, pivotIndex - 1);
            quickSort(lines, pivotIndex + 1, high);
        }
    }

    private int partition(List<string> lines, int low, int high)
    {
        int pivotIndex;
        if (low < lines.Count && high > 0)
        {
            pivotIndex = selectPivot(lines, low, high);
        }
        else
        {
            pivotIndex = high;
        }

        /*
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (IsLess(pivot, lines[j]))
            {
                i = i + 1;
                string loopTemp = lines[i];
                lines[i] = lines[j];
                lines[j] = loopTemp;
            }
        }

        string temp = lines[i + 1];
        lines[i + 1] = lines[high];
        lines[high] = temp;
        return i + 1;
        */
        string pivot = lines[pivotIndex];

        Swap(lines, pivotIndex, high);

        int storeIndex = low;

        for (int i = low; i < high; i++)
        {
            if (IsLess(lines[i], pivot))
            {
                Swap(lines, i, storeIndex);
                storeIndex++;
            }
        }

        Swap(lines, storeIndex, high);

        return storeIndex;
    }

    private int selectPivot(List<string> lines, int low, int high)
    {
        // implement user choice of pivot
        int pivotIndex = high;

        return pivotIndex;
    }

    private void Swap(List<string> arr, int first, int second)
    {
        string temp = arr[first];
        arr[first] = arr[second];
        arr[second] = temp;
    }
}
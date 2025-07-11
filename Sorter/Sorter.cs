// Sorter Class, contains all sorting logic
namespace Sorter
{
    public class Sorter
    {
        // variables
        string listName, savesFolder;
        List<ComparedStrings> savedComparisons;
        List<int> savedPivots;
        List<string> startingList, workingList;
        int pivotNum;
        bool IsQuit = false;
        // constructor
        public Sorter(List<string> workingList, string listName, string savesFolder, List<ComparedStrings>? savedComparisons = null, List<int>? savedPivots = null)
        {
            this.savedComparisons = savedComparisons ?? new List<ComparedStrings>();
            this.savedPivots = savedPivots ?? new List<int>();
            startingList = new List<string>(workingList);
            this.workingList = workingList;
            this.listName = listName;
            this.savesFolder = savesFolder;
        }

        // start sorting of workingList
        public bool UserSort()
        {
            QuickSort(0, workingList.Count - 1);
            if (IsQuit) return false;
            return true;
        }

        // recursive quick sort
        private void QuickSort(int low, int high)
        {
            if (IsQuit) return;

            if (low < high)
            {
                int pivotIndex = Partition(low, high);
                QuickSort(low, pivotIndex - 1);
                QuickSort(pivotIndex + 1, high);
            }
        }

        // sorts range into two partition and returns where the pivot is
        private int Partition(int low, int high)
        {
            // find pivot
            int pivotIndex = SelectPivot(low, high);
            string pivotValue = workingList[pivotIndex];

            // temporarily put pivot at end
            Utils.Swap(workingList, pivotIndex, high);

            // place values smaller than pivot at beginning of list 
            int storeIndex = low;
            for (int i = low; i < high; i++)
            {
                if (IsGreater(pivotValue, workingList[i]))
                {
                    Utils.Swap(workingList, i, storeIndex);
                    storeIndex++;
                }
                if (IsQuit) return -1;
            }

            // put pivot after all values determined to be lower
            Utils.Swap(workingList, storeIndex, high);

            return storeIndex;
        }


        // selects a pivot based off saved pivots or user input 
        private int SelectPivot(int low, int high)
        {
            // variables
            int numElements = high - low;
            int pivotIndex = (high + low) / 2; // default pivotIndex

            
            if (pivotNum < savedPivots.Count)
            {
                // if pivot already stored use that
                pivotIndex = savedPivots[pivotNum];
            }
            else if (numElements == 2)
            {
                // if two elements just use default
                savedPivots.Add(pivotIndex);
            }
            else
            {
                // get user to select pivot
                pivotIndex = Utils.UserSelectFromList(workingList.ToArray(), low, high, "please select the middle most element");
                savedPivots.Add(pivotIndex);
            }
            pivotNum++;

            return pivotIndex;
        }

        // determine if one value is larger than the other, either through saved list or user choice
        private bool IsGreater(string maybeGreater, string maybeLesser)
        {
            int userInput = 0;

            // check if comparison is stored and return that value if it is
            foreach (var comparison in savedComparisons)
            {
                int checkValue = comparison.Check(maybeGreater, maybeLesser);
                if (checkValue == 0) return true;
                else if (checkValue == 1) return false;
            }
            // get input from user, 1 for option1, 2 for option2, 0 to save
            Console.WriteLine("options: ");
            Console.WriteLine("1: " + maybeGreater);
            Console.WriteLine("2: " + maybeLesser);
            Console.WriteLine("Enter index of largest value or 0 to save");
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 0 || userInput > 2)
            {
                Console.WriteLine("Please enter a valid number");
            }
            

            // save if user chose to
            if (userInput == 0)
            {
                Save(); // set isquit true
                return false;
            }


            // translate user choice to bool
            bool IsGreater = userInput == 1;

            StoreComparison(IsGreater, maybeGreater, maybeLesser);

            return IsGreater;
        }

        // save data to file and exit sorting
        private void Save()
        {
            List<string> lines = [];

            // add data to lines
            lines.Add(listName);
            lines.Add(ComparisonsToString(savedComparisons));
            lines.Add(string.Join(" ", savedPivots));
            foreach (var value in startingList)
            {
                lines.Add(value);
            }

            // write lines to savesFolder/listname_whateverWriteToListmayadd
            Utils.WriteListToFile(lines, savesFolder, listName);

            IsQuit = true;
        }

        // add a comparison to saved comparisons
        private void StoreComparison(bool IsGreater, string maybeGreater, string maybeLesser)
        {
            string larger, smaller;
            if (IsGreater) (larger, smaller) = (maybeGreater, maybeLesser);
            else (larger, smaller) = (maybeLesser, maybeGreater);

            ComparedStrings newComparison = new ComparedStrings(larger, smaller);

            // if comparison is already saved return
            if (savedComparisons.Contains(newComparison)) return;
    
            savedComparisons.Add(newComparison);

            // add additional comparisons by infering if a>b and b>c then a>c 
            UpdateComparisons();
        }

         
        // add additional comparisons by infering if a>b and b>c then a>c 
        private void UpdateComparisons()
        {
           for (int i = 0; i < savedComparisons.Count; i++) {
                for (int j = 0; j < savedComparisons.Count; j++)
                {
                    if (savedComparisons[i].smaller == savedComparisons[j].larger)
                    {
                        StoreComparison(true, savedComparisons[i].larger, savedComparisons[j].smaller);
                    }
                }
            }
        }

        // convert list of comparisons to string read to be stored
        private string ComparisonsToString(List<ComparedStrings> toConvert)
        {
            string returnString = "";

            // format as "(larger1,smaller1) (larger2,smaller2) ..."
            foreach (var comparison in toConvert)
            {
                returnString += comparison.ToString() + " ";
            }

            // trim off extra space
            returnString = returnString.TrimEnd();

            return returnString;
        }
    }
}
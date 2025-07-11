// General Use Functions

namespace Sorter
{
    public static class Utils
    {
        // receive and return a int from the user between min and max
        public static int UserIntInput(int min, int max)
        {
            // get user to select integer between min and max
            int returnValue;

            while (!int.TryParse(Console.ReadLine(), out returnValue) || returnValue < min || returnValue > max)
            {
                Console.WriteLine($"Invalid, please enter an integer between {min} and {max}");
            }

            return returnValue;
        }

        // ask and return a index of item from list from user between min and max, optional prompt message
        public static int UserSelectFromList(string[] list, int min = 0, int? maybeMax = null, string message = "please select index of desired item")
        {
            // get user to select an item from list, returns selected index
            int selectionIndex;
            int max = maybeMax ?? list.Length - 1;

            Console.WriteLine("Options:");
            for (int i = min; i <= max; i++)
            {
                Console.WriteLine($"{i}: {list[i]}");
            }

            Console.WriteLine(message);
            selectionIndex = UserIntInput(min, max);

            return selectionIndex;
        }
        // Output list to folderPath/listname_saved_date_time.txt
        public static void WriteListToFile(List<string> listToSave, string folderPath, string listName = "")
        {
            // output listToSave to file at folderPath/listName_sorted_date_time.txt
            string outputFilePath = Path.Combine(folderPath, listName + "_saved_" + DateTime.Now.ToString("MM-dd-yy_HH-mm") + ".txt");

            File.WriteAllLines(outputFilePath, listToSave);
        }

        // simply swap the values at index1 and index 2 in list
        public static void Swap(List<string> list, int index1, int index2)
        {
            string temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
 
    }


}
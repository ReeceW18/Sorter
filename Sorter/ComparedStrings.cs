namespace Sorter
{
    public struct ComparedStrings
    {
        // variables
        public string larger;
        public string smaller;

        // constructor
        public ComparedStrings(string larger, string smaller)
        {
            this.larger = larger;
            this.smaller = smaller;
        }

        public override string ToString()
        {
            return $"({larger},{smaller})";
        }

        // check if a value matches either member variable
        public bool Has(string value)
        {
            return value == larger || value == smaller;
        }

        // check to see if values have been compared
        // return 0 if first is greater, 1 if second is greater, -1 if comparison doesn't exit
        public int Check(string value1, string value2) {
            if (Has(value1) && Has(value2))
            {
                if (value1 == larger)
                {
                    return 0;
                }
                return 1;
            }
            return -1;
        }
        

    }
}
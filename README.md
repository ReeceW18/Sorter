# Sort Helper CLI

A command line application that helps the user sort subjective lists, written as first non-trivial c# project

## Features
- Subjecting sorting of lists based off user input
    - uses a implementation of quick sort modified for subjective user rankings
    - stores comparisons to reduce those made by user
- Support for any list of strings
- Supports saving during sorting to return to sorting later

## Setup

External use is not necessarily maintained. 

1. Prerequisites
This project requires [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later. Check for installation with:
```
dotnet --version
```

2. Clone Repository
git clone https://github.com/ReeceW18/Sorter.git

3. Build
```
dotnet build
```

4. Run
```
dotnet run
```

## Use

1. Run once to create folders
2. Folder "SubjectiveSortingHelper" is added to OS myDocuments folder all files are stored here, put files you want to sort in /privateLists
    - File should contain a different item to be sorted on each line
3. Select 1 to sort new file
4. Select file index of file to be sorted
5. Select roughly middle most element, don't overthink it. A better choice will lead to less choices later but do not let the decision overwhelm you
6. Select larger element index or select 0 to save current sorting state and return to start
7. repeat 5 and 6 until sorted
8. Sorted file is stored in /sortedLists

## Design

Base sorting algorithm is quick sort, where the user selects the pivots. If the user selects sufficiently mid elements this allows for fewer comparisons than by a simple merge sort.

Stores comparisons as the user makes them and infers further comparisons from transitive relationships. Allows for much fewer comparisons made by user.

## Future Work
- Officially support external use
- Further clean up code
    - plenty of places with subpar logic or poor following of programming conventions
- Improve UI, particularly displaying lists
- Develop more clever ways to sort when involving user input
- Store comparisons as a directed graph instead of current messy implementation
- Consider edge cases and bugs that may arise from users imperfect sorting
    - are there cases where the user can create contradictory rankings?

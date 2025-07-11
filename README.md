# Sort Helper CLI

A command line application that helps the user sort subjective lists, written as first non-trivial c# project

## Features
- Subjecting sorting of lists based off user input
    - uses a implementation of quick sort modified for subjective user rankings
    - stores comparisons to reduce those made by user
- Support for any list of strings
- Supports saving during sorting to return to sorting later

## Setup

External use is not currently supported. The following instructions are if you manage to get code to run. Requires NET 8.0.

1. Put lists to sort in /ProgramFiles/privateLists/ as a .txt file where each line is an element to sort
2. Run application, and follow given instructions
3. Sorted lists are stored in /ProgramFiles/sortedLists/

## Design

Base sorting algorithm is quick sort, where the user selects the pivots. If the user selects sufficiently mid elements this allows for fewer comparisons than by a simple merge sort.

Stores comparisons as the user makes them and infers further comparisons from transitive relationships. Allows for much fewer comparisons made by user.

## Future Work
- Officially support external use.
- Further clean code
    - plenty of places with subpar logic or poor following of programming conventions
- Improve UI, particularly displaying lists
- Develop more clever ways to sort when involving user input
- Store comparisons as a directed graph instead of current messy implementation

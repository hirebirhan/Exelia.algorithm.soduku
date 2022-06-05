
/*
* Given a Sudoku data structure with size NxN, N > 0 and vN == integer, write a method to validate if it has been filled out correctly.
* The data structure is a multi-dimensional Array, ie:

[
  [7,8,4,  1,5,9,  3,2,6],
  [5,3,9,  6,7,2,  8,4,1],
  [6,1,2,  4,3,8,  7,5,9],

  [9,2,8,  7,1,5,  4,6,3],
  [3,5,7,  8,4,6,  1,9,2],
  [4,6,1,  9,2,3,  5,8,7],

  [8,7,6,  3,9,4,  2,1,5],
  [2,4,3,  5,6,1,  9,7,8],
  [1,9,5,  2,8,7,  6,3,4]
]


* Rules for validation

* Data structure dimension: NxN where N > 0 and vN == integer
* Rows may only contain integers: 1..N (N included)
* Columns may only contain integers: 1..N (N included)
* 'Little squares' (3x3 in example above) may also only contain integers: 1..N (N included)

* taken from http://www.codewars.com/kata/540afbe2dc9f615d5e000425/train/javascript
*/

using System.Diagnostics;

namespace Exelia.algorithm.soduku
{
    class SudokuPuzzleValidator
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            Debug.Assert(ValidateSudoku(goodSudoku1), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1), "This isn't supposed to validate! It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2), "This isn't supposed to validate! It's a bad sudoku!");

        }
        static bool IsSquare(int[][] puzzle)
        {
            int length = puzzle.Length;
            for (int row = 0; row < length;)

            {
                if (puzzle[row].Length != length) return false;
                row++;
            }
            return true;
        }

        static bool IsValidRow(int[] rowElements, int number)
        {
            return rowElements.Where(element => element == number).Count() <= 1;
        }

        static bool IsValidGrid(int[][] puzzle, int row, int col, int number)
        {
            int r = (row / 3) * 3;
            int c = (col / 3) * 3;
            int elementCount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[r + i][j + c] == number)
                    {
                        elementCount++;
                    }
                }
            }
            return elementCount <= 1;
        }

        static bool ValidateSudoku(int[][] puzzle)
        {
            if (!IsSquare(puzzle)) return false;

            int length = puzzle.Length;



            //checking rows if there is any duplicate
            for (int row = 0; row < length; row++)
            {
                int[] numbersInRow = puzzle[row];
                for (int col = 0; col < length; col++)
                {
                    int columnValue = puzzle[row][col];
                    if (!IsValidRow(numbersInRow, columnValue)) return false;

                }

            }

            //checking column if there is any duplicate
            for (int col = 0; col < length; col++)
            {
                int[] numbersInColumn = new int[length];
                for (int row = 0; row < length; row++)
                {
                    //creating an array from columns
                    int numberInCol = puzzle[row][col];
                    numbersInColumn[row] = numberInCol;
                    if (!IsValidRow(numbersInColumn, numberInCol)) return false;

                }

            }
            if (puzzle.Length > 6)
            {

                for (int row = 0; row < length; row++)
                {
                    for (int col = 0; col < length; col++)
                    {
                        int numberInCol = puzzle[row][col];
                        if (!IsValidGrid(puzzle, row, col, numberInCol)) return false;
                    }
                }
            }
            return true;
        }
    }

}

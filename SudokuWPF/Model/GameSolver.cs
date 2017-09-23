using System.Collections.Generic;

namespace SudokuWPF.Solver
{
	public class SudokuSolver
	{
		public int[,] grid;

		public SudokuSolver(int[,] grid)
		{
			this.grid = grid;
		}

		public bool solveSudoku(int[,] grid)
		{
			int row = 0;
			int col = 0;

			// If no blank space has been found, return true in solveSudoku method
			if (!FindBlankSpace(grid, ref row, ref col))
			{
				return true;
			}

			else // if blank space has been found...
			{
				for (int num = 1; num <= 9; num++)
				{
					if (!Conflict(grid, ref row, ref col, ref num))
					{
						grid[row, col] = num;
						if (solveSudoku(grid))
						{
							return true;
						}
						grid[row, col] = 0;
					}
				}
				return false;
			}
		}

		private bool Conflict(int[,] grid, ref int row, ref int col, ref int num)
		{
			Dictionary<int, bool> dicForRow = new Dictionary<int, bool>();
			Dictionary<int, bool> dicForCol = new Dictionary<int, bool>();
			Dictionary<int, bool> dicForBox = new Dictionary<int, bool>();

			dicForCol.Add(num, true);

			// Add bool return type
			// key search
		}

		public bool FindBlankSpace(int[,] grid, ref int row, ref int col)
		{
			for (int i = 0; i <= 8; i++)
			{
				for (int j = 0; j <= 8; j++)
				{
					if (grid[i, j] == 0)
					{
						row = i;
						col = j;

						return true;
					}
				}
			}
			return false;
		}
	}
}

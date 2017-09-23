using System.Collections.Generic;

namespace SudokuWPF.Solver
{
	public class SudokuSolver
	{
		private int cap;
		public int[,] grid;

		public SudokuSolver(int cap, int[,] grid)
		{
			this.cap = cap;
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

			for (int i = 0; i <= 8; i++)
			{
				// .ContainsKey() : Dictionary 매소드로 검사한 값에 지정한 값이 있는 지 검사합니다.
				if (grid[i, col] != 0 && !dictForCol.ContainsKey(grid[i, col]))
					dictForCol.Add(grid[i, col], true);
				else if (dictForCol.ContainsKey(grid[i, col]))
					return true;
			}

			// .Add() : 특정값을 Dictionary 매소드에 삽입합니다.
			dictForRow.Add(num, true);

			for (int i = 0; i <= 8; i++)
			{
				if (grid[row, i] != 0 && !dictForRow.ContainsKey(grid[row, i]))
					dictForRow.Add(grid[row, i], true);
				else if (dictForRow.ContainsKey(grid[row, i]))
					return true;
			}

			dictForBox.Add(num, true);

			int xStartOfBox = 0;
			int yStartOfBox = 0;
			if (row >= 0 && row <= 2)
				xStartOfBox = 0;
			else if (row >= 3 && row <= 5)
				xStartOfBox = 3;
			else
				xStartOfBox = 6;


			if (col >= 0 && col <= 2)
				yStartOfBox = 0;
			else if (col >= 3 && col <= 5)
				yStartOfBox = 3;
			else
				yStartOfBox = 6;

			for (int i = xStartOfBox; i < xStartOfBox + 3; i++)
			{
				for (int j = yStartOfBox; j < yStartOfBox + 3; j++)
				{
					if (grid[i, j] != 0 && !dictForBox.ContainsKey(grid[i, j]))
						dictForBox.Add(grid[i, j], true);
					else if (dictForBox.ContainsKey(grid[i, j]))
						return true;
				}
			}
			return false;
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

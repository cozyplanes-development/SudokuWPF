using System.Collections.Generic;

namespace SudokuWPF.Solver
{
	public class GameSolver
	{
		private int cap;
		public int[,] grid;

		/// <summary>
		/// 현재 클래스의 cap 과 param 을 검사합니다.
		/// </summary>
		/// <param name="cap"></param>
		/// <param name="grid">스도쿠의 grid</param>
		public GameSolver(int cap, int[,] grid)
		{
			this.cap = cap;
			this.grid = grid;
		}

		/// <summary>
		/// 각각의 grid를 순환하며 값을 찾아냅니다.
		/// </summary>
		/// <param name="grid">스도쿠의 grid</param>
		/// <returns></returns>
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

		/// <summary>
		/// 전체 Grid를 각각 검사하면서 중복되는 값, 아직 지정되지 않은 값을 검사후 값을 지정합니다.
		/// </summary>
		/// <param name="grid">스도쿠의 Grid</param>
		/// <param name="row">(Reference) Grid의 열</param>
		/// <param name="col">(Reference) Grid의 행</param>
		/// <param name="num">(Reference) Grid의 숫자값</param>
		/// <returns>Dictionary 메소드로 Grid에 값을 지정한 후 값/boolean을 반환합니다.</returns>
		private bool Conflict(int[,] grid, ref int row, ref int col, ref int num)
		{
			Dictionary<int, bool> dicForRow = new Dictionary<int, bool>();
			Dictionary<int, bool> dicForCol = new Dictionary<int, bool>();
			Dictionary<int, bool> dicForBox = new Dictionary<int, bool>();

			dicForCol.Add(num, true);

			for (int i = 0; i <= 8; i++)
			{
				// .ContainsKey() : Dictionary 매소드로 검사한 값에 지정한 값이 있는 지 검사합니다.
				if (grid[i, col] != 0 && !dicForCol.ContainsKey(grid[i, col]))
					dicForCol.Add(grid[i, col], true);
				else if (dicForCol.ContainsKey(grid[i, col]))
					return true;
			}

			// .Add() : 특정값을 Dictionary 매소드에 삽입합니다.
			dicForRow.Add(num, true);

			for (int i = 0; i <= 8; i++)
			{
				if (grid[row, i] != 0 && !dicForRow.ContainsKey(grid[row, i]))
					dicForRow.Add(grid[row, i], true);
				else if (dicForRow.ContainsKey(grid[row, i]))
					return true;
			}

			dicForBox.Add(num, true);

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
					if (grid[i, j] != 0 && !dicForBox.ContainsKey(grid[i, j]))
						dicForBox.Add(grid[i, j], true);
					else if (dicForBox.ContainsKey(grid[i, j]))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 아직 Grid에 지정되지 않은 값을 확인합니다.
		/// </summary>
		/// <param name="grid">스도쿠의 Grid</param>
		/// <param name="row">(Reference) Grid의 열</param>
		/// <param name="col">(Reference) Grid의 행</param>
		/// <returns>모두 수행한 후 boolean 값을 반환합니다.</returns>
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

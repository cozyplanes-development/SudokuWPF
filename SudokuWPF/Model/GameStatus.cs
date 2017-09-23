using System.Collections.Generic;

namespace SudokuWPF.Status
{
	class GameStatus
	{
		public bool checkGameStatus(int[,] grid)
		{
			// needs to understand code here
			for (int i = 0; i < 9; i++)
			{
				int[] row = new int[9];
				int[] square = new int[9];
				int[] column = new int[9]; //Equals with : (int[]) grid[i].Clone();

				for (int j = 0; j < 9; j++)
				{
					row[j] = grid[j, i];
					column[j] = grid[i, j];
					square[j] = grid[(i / 3) * 3 + j / 3, i * 3 % 9 + j % 3]; // Some grid calculation function
				}
				if (!(validate(column) && validate(row) && validate(square)))
					return false;
			}
			return true;
		}

		private bool validate(int[] check)
		{
			List<int> check_list = new List<int>(check);
			check_list.Sort();

			int i = 0;
			foreach (int number in check_list)
			{
				if (number != ++i)
				{
					return false;
				}
			}
			return true;
		}
	}
}

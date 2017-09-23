using System.Collections.Generic;

namespace SudokuWPF.Status
{
	class GameStatus
	{
		public bool checkGameStatus(int[,] grid)
		{
			// function returning bool param 'grid'

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

using System;

namespace SudokuWPF.Generator
{
	class GameGenerator
	{
		// Board의 크기 지정
		public static int BOARD_WIDTH = 9; // 가로
		public static int BOARD_HEIGHT = 9; // 세로

		// Sudoku의 Board 배열
		int[,] board;
		public int[,] Board
		{
			get
			{
				return board;
			}
			set
			{
				board = value;
			}
		}

		// Sudoku Board 의 Constructor
		public SudokuGenerator()
		{
			Board = new int[BOARD_WIDTH, BOARD_HEIGHT];
		}

		// Driver method for nextBoard
		public int[,] nextBoard(int difficulty)
		{
			Board = new int[BOARD_WIDTH, BOARD_HEIGHT];
			nextCell(0, 0);
			makeHoles(difficulty);
			return Board;
		}

		// Recursive function to put number in every cell
		public bool nextCell(int x, int y)
		{
			int nextX = x;
			int nextY = y;
			int[] forCheck = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Random random = new Random();
			int temp = 0;
			int current = 0;
			int top = forCheck.Length;
			
			for (int i = 0; i < forCheck.Length; i++)
			{
				if (legalMove(x, y, forCheck[i]))
				{
					if (x == 8)
					{
						if (y == 8)
						{
							return true;
						}

						else
						{
							nextX = 0;
							nextY = y + 1;
						}
					}

					else
					{
						nextX = x + 1;
					}

					if (nextCell(nextX, nextY))
					{
						return true;
					}
				}
			}

			Board[x, y] = 0;
			return false;

			Board[x, y] = 0;
			return false;
		}

		private bool legalMove(int x, int y, int currentNum)
		{
			for (int i = 0; i < 9; i++)
			{
				if (currentNum == Board[x, i])
				{
					return false;
				}
			}

			for (int i = 0; i < 9; i++)
			{
				if (currentNum == Board[i, y])
				{
					return false;
				}
			}

			int cornerX = 0;
			int cornerY = 0;

			if (x > 2)
			{
				if (x > 5)
				{
					cornerX = 6;
				}
				else
				{
					cornerX = 3;
				}
			}
				
			if (y > 2)
			{
				if (y > 5)
				{
					cornerY = 6;
				}
				else
				{
					cornerY = 3;
				}
			}

			for (int i = cornerX; i < 10 && i < cornerX + 3; i++)
			{
				for (int j = cornerY; j < 10 && j < cornerY + 3; j++)
				{
					if (currentNum == Board[i, j])
					{
						return false;
					}
				}
			}	
			return true;
		}

		public void makeHoles(int holesToMake)
		{
			double remainingHoles = (double)holesToMake;
			Random rand = new Random();
			while (remainingHoles > 0)
			{
				int i = rand.Next(0, 9);
				int j = rand.Next(0, 9);
				if (Board[i, j] != 0)
				{
					Board[i, j] = 0;
					remainingHoles--;
				}
			}
		}
	}
}

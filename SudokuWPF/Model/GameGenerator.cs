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

		/// <summary>
		/// Constructor입니다. 
		/// Board의 모든 값을 0으로 초기화합니다.
		/// </summary>
		public GameGenerator()
		{
			Board = new int[BOARD_WIDTH, BOARD_HEIGHT];
		}

		/// <summary>
		/// nextBoard를 위한 드라이버 매소드
		/// </summary>
		/// <param name="difficulty">난이도, 빈칸의 개수로 생각함</param>
		/// <returns>완성되지 않은 Board</returns>
		public int[,] nextBoard(int difficulty)
		{
			Board = new int[BOARD_WIDTH, BOARD_HEIGHT];
			nextCell(0, 0);
			makeHoles(difficulty);
			return Board;
		}

		/// <summary>
		/// 모든 셀에 수를 삽입하기 위한 재귀 함수
		/// </summary>
		/// <param name="x">현재 셀의 x값</param>
		/// <param name="y">현재 셀의 y값</param>
		/// <returns>하나의 해답이 나온다면 true, 해답이 나오지 않는다면 false를 반환합니다.</returns>
		public bool nextCell(int x, int y)
		{
			int nextX = x;
			int nextY = y;
			int[] forCheck = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Random random = new Random();
			//int temp = 0;
			//int current = 0;
			int top = forCheck.Length;
			
			for (int i = 0; i < forCheck.Length; i++)
			{
				if (legalMove(x, y, forCheck[i]))
				{
					Board[x, y] = forCheck[i];
					if (x == 8)
					{
						if (y == 8)
						{
							return true; // 끝!
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
		}

		/// <summary>
		/// 해당 셀의 좌표와 가능한 숫자가 주어지면 
		/// 해당 숫자가 해당 셀에 합법적으로 삽입 될 수 있는지를 결정합니다.
		/// </summary>
		/// <param name="x">셀의 x값</param>
		/// <param name="y">셀의 y값</param>
		/// <param name="current">해당 셀에서 확인할 값</param>
		/// <returns>current가 적법하다면 true, 아니면 false를 반환합니다.</returns>
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

		/// <summary>
		/// 완성된 Board가 주어지면 사전에 정해진 수 만큼 0을 셀에 삽입한다.
		/// </summary>
		/// <param name="holesToMake">Board에 삽입할 0의 개수</param>
		public void makeHoles(int holesToMake)
		{
			/* 
			 * 이 게임에서는 난이도를 다음과 같이 설정합니다.
			 * Easy: 32+ 개의 수가 주어짐 (49개 이하의 빈칸)
			 * Medium: 27-31개의 수가 주어짐 (50-54개의 빈칸)
		     * Hard: 26개 이하의 수가 주어짐 (54+ 개의 빈칸)
				
			 * 이 난이도는 알고리즘과 아무런 관계가 없습니다. 
			 * 단순 사람의 능력치라고 보시면 됩니다.
             */
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

using System;
using SudokuWPF.Generator;
using SudokuWPF.Status;
using SudokuWPF.Solver;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SudokuWPF.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        List<Cell> cells = new List<Cell>();
        int numRows = 9;
        int numColumns = 9;
        int[,] BoardInitial;
        int[,] BoardSolved;
        int[,] BoardCurrent;
        int numHoles = 40;
        bool solutionBtnPressed = false;
        List<string> difficultyList = new List<string>();
        string selectedDifficulty = DIFFICULTY_EASY;
        const string DIFFICULTY_EASY = "Easy";
        const string DIFFICULTY_INTERMEDIATE = "Intermediate";
        const string DIFFICULTY_DIFFICULTY = "Difficult";

        public MainViewModel()
        {
            difficultyList = new List<string>();
            difficultyList.Add(DIFFICULTY_EASY);
            difficultyList.Add(DIFFICULTY_INTERMEDIATE);
            difficultyList.Add(DIFFICULTY_DIFFICULTY);

            NewGame();
        }

        private void NewGame()
        {
            numHoles = GetNumHolesByDifficulty();

            solutionBtnPressed = false;
            GameGenerator generate = new GameGenerator();
            bool solved = false;
            do
            {
				generate.nextBoard(numHoles);
                BoardInitial = generate.Board;
                BoardSolved = (int[,])generate.Board.Clone();
                SudokuSolver ssolver = new SudokuSolver(9, BoardSolved);
                solved = ssolver.solveSudoku(BoardSolved);
            }
            while (!solved);

            cells = new List<Cell>();
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Cell c = new Cell();
                    int index = i * NumRows + j;
                    int value = generate.Board[i, j];
                    if (value != 0)
                    {
                        c.Text = value.ToString();
                        c.IsFixed = true;
                    }

                    else
                    {
                        c.Text = "";
                        c.IsFixed = false;
                    }
                    c.Index = index;
                    c.Number = value;
                    c.Row = i;
                    c.Column = j;
                    c.NumberChanged += CellVMNumberChanged;
                    c.UpdateBackground();
                    cells.Add(c);
                }
            }

            RaisePropertyChanged("cells");
        }

        private int GetNumHolesByDifficulty()
        {
            switch (selectedDifficulty)
            {
                case DIFFICULTY_EASY:
                    return 20;
                case DIFFICULTY_INTERMEDIATE:
                    return 30;
                case DIFFICULTY_DIFFICULTY:
                    return 40;
            }
            return 40; 
        }

        private void CellVMNumberChanged(object sender, NumberChangedEventArgs e)
        {
            if (!solutionBtnPressed)
            {
                BoardCurrent = new int[9, 9];
                foreach (Cell c in cells)
                    BoardCurrent[c.Row, c.Column] = c.Number;

                GameStatus gamestatus = new GameStatus();
                if (gamestatus.checkGameStatus(BoardCurrent))
                {
                    MessageBox.Show("You Won!");
                }
                else if (IsCompleted())
                {
                    MessageBox.Show("The solution isn't correct. :(");
                }
            }

        }

        public bool IsCompleted()
        {
            Cell c = cells.Where(e => e.Number == 0).FirstOrDefault();
            if (c != null)
			{
				return false;
			}
            return true;
        }

		#region List: Cells
		public List<Cell> Cells
        {
            get
            {
                return cells;
            }

            set
            {
                cells = value;
                RaisePropertyChanged("cells");
            }
        }
		#endregion

		#region List: DifficultyList
		public List<string> DifficultyList
		{
			get
			{
				return difficultyList;
			}

			set
			{
				difficultyList = value;
				RaisePropertyChanged("difficultyList");
			}
		}
		#endregion

		#region Rows
		public int NumRows
        {
            get
            {
                return numRows;
            }

            set
            {
                numRows = value;
                RaisePropertyChanged("NumRows");
            }
        }
		#endregion

		#region Columns
		public int NumColumns
        {
            get
            {
                return numColumns;
            }

            set
            {
                numColumns = value;
                RaisePropertyChanged("NumColumns");
            }
        }
		#endregion

		#region SelectedDifficulty
		public string SelectedDifficulty
        {
            get
            {
                return selectedDifficulty;
            }

            set
            {
                selectedDifficulty = value;
                RaisePropertyChanged("selectedDifficulty");
            }
        }
		#endregion

		#region Top bar onClick methods

		#region ClickNewGameCommand

		private ICommand _clickNewGameCommand;
        public ICommand ClickNewGameCommand
        {
            get
            {
                return _clickNewGameCommand ?? (_clickNewGameCommand = new CommandHandler(() => MyActionNewGame(), CanExecuteActionNewGame()));
            }
        }

        private bool CanExecuteActionNewGame()
        {
            return true;
        }

        public void MyActionNewGame()
        {
            NewGame();
        }
        #endregion

        #region ClickHintCommand

        private ICommand _clickHintCommand;
        public ICommand ClickHintCommand
        {
            get
            {
                return _clickHintCommand ?? (_clickHintCommand = new CommandHandler(() => MyActionHint(), CanExecuteActionHint()));
            }
        }

        private bool CanExecuteActionHint()
        {
            return true;
        }

        public void MyActionHint()
        {
            Cell c = cells.Where(e => e.Number == 0).FirstOrDefault();
            if (c != null)
            {
                c.Text = BoardSolved[c.Row, c.Column].ToString();
                c.Number = BoardSolved[c.Row, c.Column];
            }
        }
        #endregion

        #region ClickSolutionCommand

        private ICommand _clickSolutionCommand;
        public ICommand ClickSolutionCommand
        {
            get
            {
                return _clickSolutionCommand ?? (_clickSolutionCommand = new CommandHandler(() => MyActionSolution(), CanExecuteActionSolution()));
            }
        }

        

        private bool CanExecuteActionSolution()
        {
            return true;
        }

        public void MyActionSolution()
        {
            solutionBtnPressed = true;
            foreach (Cell c in cells)
            {
                c.Text = BoardSolved[c.Row, c.Column].ToString();
                c.Number = BoardSolved[c.Row, c.Column];
            }
        }
		#endregion

		#endregion

		#region Dealing with Changed Property
		public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
		#endregion
	}

	#region Command/Event Handlers
	public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
	#endregion
}


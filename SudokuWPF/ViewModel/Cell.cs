using System;
using System.ComponentModel;
using System.Windows.Media;

namespace SudokuWPF.ViewModel
{
    class Cell : INotifyPropertyChanged
	// INotifyPropertyChanged : Notifies clients that a property value has changed.
	{
		string text = string.Empty;
        int row;
        int column;
        SolidColorBrush background = Brushes.Gold;
        bool isFixed = false;
        int _number;
        int index;

		#region Background Color Update
		public void UpdateBackground()
        {
            if (row <= 2 && column <= 2)
                background = Brushes.LightBlue;
            if (row <= 2 && column >= 3 && column <=5)
                background = Brushes.LightGreen;
            if (row <= 2 && column >=6)
                background = Brushes.LightBlue;

            if (row >=3 && row <=5 && column <= 2)
                background = Brushes.LightGreen;
            if (row >= 3 && row <= 5 && column >= 3 && column <= 5)
                background = Brushes.LightBlue;
            if (row >= 3 && row <= 5 && column >= 6)
                background = Brushes.LightGreen;

            if (row >= 6 && column <= 2)
                background = Brushes.LightBlue;
            if (row >= 6 && column >= 3 && column <= 5)
                background = Brushes.LightGreen;
            if (row >= 6 && column >= 6)
                background = Brushes.LightBlue;
        }
		#endregion

		#region String: Text
		public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                RaisePropertyChanged("Text");

                _number = 0;
                int.TryParse(text, out _number);
                RaisePropertyChanged("Number");
                RaiseNumberChanged(new NumberChangedEventArgs(_number));

            }
        }
		#endregion

		#region Row
		public int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
                RaisePropertyChanged("Row");
            }
        }
		#endregion

		#region Column
		public int Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
                RaisePropertyChanged("Column");
            }
        }
		#endregion

		#region SolidColorBrush: Background
		public SolidColorBrush Background
        {
            get
            {
                return background;
            }

            set
            {
                background = value;
                RaisePropertyChanged("Background");
            }
        }
		#endregion

		#region IsFixed
		public bool IsFixed
        {
            get
            {
                return isFixed;
            }

            set
            {
                isFixed = value;
                RaisePropertyChanged("IsFixed");
            }
        }
		#endregion

		#region Number
		public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
                RaisePropertyChanged("Number");
                RaiseNumberChanged(new NumberChangedEventArgs(_number));
            }
        }
		#endregion

		#region Index
		public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
                RaisePropertyChanged("Index");
            }
        }
		#endregion

		#region Various Handlers
		public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event EventHandler<NumberChangedEventArgs> NumberChanged;
        private void RaiseNumberChanged(NumberChangedEventArgs e)
        {
            var handler = NumberChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
		#endregion
	}
}

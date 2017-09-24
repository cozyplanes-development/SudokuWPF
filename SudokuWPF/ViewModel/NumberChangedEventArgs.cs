using System;

namespace SudokuWPF.ViewModel
{
    public class NumberChangedEventArgs : EventArgs
    {
        #region Constructors

        public NumberChangedEventArgs(int number)
        {
            Number = number;
        }

        #endregion Constructors

        #region Public Properties

        public int Number
        {
            get; private set;
        }

        #endregion Public Properties
    }
}

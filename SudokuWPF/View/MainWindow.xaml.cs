using SudokuWPF.ViewModel;
using System.Windows;

namespace SudokuWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = new MainViewModel();
		}
	}
}

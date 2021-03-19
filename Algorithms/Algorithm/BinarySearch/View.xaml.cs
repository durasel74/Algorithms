using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Algorithms.Algorithm.BinarySearch
{
	public partial class View : Window
	{
		BSViewModel viewModel;
		public View()
		{
			InitializeComponent();
			DataContext = new BSViewModel();
			viewModel = (BSViewModel)DataContext;
			numbersView.Container.SelectionChanged += NumbersView_SelectionChanged;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			viewModel.RestartCommand.Execute(null);
			var mainWindow = new MainWindow();
			mainWindow.Show();
		}
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Keyboard.ClearFocus();
		}

		private void NumbersView_SelectionChanged(object sender, 
			SelectionChangedEventArgs e)
		{
			BinarySearch alg = (BinarySearch)viewModel.Algorithm;
			if (alg.IsRunning)
			{
				for (int i = 0; i < alg.Low - 1; i++)
				{
					var item = (ListBoxItem)numbersView.Container.
						ItemContainerGenerator.ContainerFromIndex(i);
					if (item != null)
						item.IsEnabled = false;
				}
				for (int i = alg.High + 2; i < alg.Array.Count; i++)
				{
					var item = (ListBoxItem)numbersView.Container.
						ItemContainerGenerator.ContainerFromIndex(i);
					if(item != null)
						item.IsEnabled = false;
				}
			}
			if (alg.ResultIndex != -1)
			{
				var algorithm = viewModel.Algorithm;
				var item = (ListBoxItem)numbersView.Container.
					ItemContainerGenerator.ContainerFromIndex(alg.ResultIndex);
				if (item != null)
					item.Background = Brushes.Green;
			}
		}
	}
}

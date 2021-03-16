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
			if (viewModel.Algorithm.IsStart)
			{
				var algorithm = viewModel.Algorithm;
				for (int i = 0; i < algorithm.Low - 1; i++)
				{
					var item = (ListBoxItem)numbersView.Container.ItemContainerGenerator.ContainerFromIndex(i);

					item.IsEnabled = false;
				}
				for (int i = algorithm.High + 2; i < algorithm.Array.Count; i++)
				{
					var item = (ListBoxItem)numbersView.Container.ItemContainerGenerator.ContainerFromIndex(i);

					item.IsEnabled = false;
				}
			}
			if (viewModel.Algorithm.Result != -1)
			{
				var algorithm = viewModel.Algorithm;
				for (int i = 0; i < algorithm.Array.Count; i++)
				{
					var item = (ListBoxItem)numbersView.Container.ItemContainerGenerator.ContainerFromIndex(i);

					if (i != algorithm.Result)
						item.IsEnabled = false;
					else
						item.Background = Brushes.Green;
				}
				viewModel.Algorithm.Result = -1;
			}
		}
	}
}

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

namespace Algorithms.Algorithm.DifficultSortings
{
	public partial class View : Window
	{
		private DSViewModel viewModel;
		public View()
		{
			InitializeComponent();
			DataContext = new DSViewModel();
			viewModel = (DSViewModel)DataContext;
			sortingView.Container.SelectionChanged += SortingView_SelectionChanged;
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

		private void SortingView_SelectionChanged(object sender,
			SelectionChangedEventArgs e)
		{
			DifficultSortings alg = (DifficultSortings)viewModel.Algorithm;
			if (!alg.IsRunning)
			{
				for (int i = 0; i < alg.Array.Count; i++)
				{
					var item = GetItemForIndex(i);
					if (item != null)
						item.IsEnabled = false;
				}
			}
		}
		private ListBoxItem GetItemForIndex(int index)
		{
			var item = (ListBoxItem)sortingView.Container.
						ItemContainerGenerator.ContainerFromIndex(index);
			return item;
		}
	}
}

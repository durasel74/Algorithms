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

namespace Algorithms.Algorithm.EasySortings
{
	public partial class View : Window
	{
		private ESViewModel viewModel;
		public View()
		{
			InitializeComponent();
			DataContext = new ESViewModel();
			viewModel = (ESViewModel)DataContext;
			//numbersView.Container.SelectionChanged += NumbersView_SelectionChanged;
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




		//private void NumbersView_SelectionChanged(object sender, 
		//	SelectionChangedEventArgs e)
		//{
		//	BinarySearch alg = (BinarySearch)viewModel.Algorithm;
		//	if (alg.IsRunning)
		//	{
		//		for (int i = 0; i < alg.Low - 1; i++)
		//		{
		//			var item = GetItemForIndex(i);
		//			if (item != null)
		//				item.IsEnabled = false;
		//		}
		//		for (int i = alg.High + 2; i < alg.Array.Count; i++)
		//		{
		//			var item = GetItemForIndex(i);
		//			if (item != null)
		//				item.IsEnabled = false;
		//		}
		//	}
		//	if (alg.ResultIndex != -1)
		//	{
		//		for (int i = 0; i < alg.Array.Count; i++)
		//		{
		//			var item = GetItemForIndex(i);
		//			if (i == alg.ResultIndex)
		//			{
		//				item.Background = (Brush)TryFindResource("NLI_Result_BGColor");
		//				item.BorderBrush = (Brush)TryFindResource("NLI_Result_BorderColor");
		//				item.Foreground = (Brush)TryFindResource("NLI_Result_FGColor");
		//				continue;
		//			}
		//			if (i != alg.ResultIndex && item != null)
		//				item.IsEnabled = false;
		//		}
		//	}
		//	else if (!alg.IsRunning)
		//	{
		//		for (int i = 0; i < alg.Array.Count; i++)
		//		{
		//			var item = GetItemForIndex(i);
		//			if (item != null)
		//				item.IsEnabled = false;
		//		}
		//	}
		//}
		//private ListBoxItem GetItemForIndex(int index)
		//{
		//	var item = (ListBoxItem)numbersView.Container.
		//				ItemContainerGenerator.ContainerFromIndex(index);
		//	return item;
		//}
	}
}

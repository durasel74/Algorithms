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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Algorithms.Resources.Views
{
	public partial class NumbersView : UserControl
	{
		private const double resizeMult = 100;
		private int ID = 1;
		
		public NumbersView()
		{
			InitializeComponent();
		}

		private void wrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			wrapPanel.MaxWidth = 200 + 60 * (wrapPanel.Children.Count / 10);

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var newBtn = new Button();
			newBtn.Content = ID;
			newBtn.Width = 50;
			newBtn.Height = 50;
			newBtn.Margin = new Thickness(2);
			wrapPanel.Children.Add(newBtn);
			ID++;
		}
		private void Button_Click2(object sender, RoutedEventArgs e)
		{
			if (wrapPanel.Children.Count > 0)
			{
				wrapPanel.Children.RemoveAt(wrapPanel.Children.Count-1);
				ID--;
			}
		}
	}
}

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
		private WrapPanel wrapPanel;
		
		public NumbersView()
		{
			InitializeComponent();
		}
		private void Container_RightMouse(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}
		private void Container_LeftMouse(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		private void WrapPanel_Loaded(object sender, RoutedEventArgs e)
		{
			WrapPanel panel = sender as WrapPanel;
			wrapPanel = panel;
			WrapPanelResize();
		}
		private void NumbersView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			WrapPanelResize();
		}
		private void WrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			WrapPanelResize();
		}
		private void WrapPanelResize()
		{
			if (wrapPanel != null)
				wrapPanel.MaxWidth = this.ActualWidth + (Container.Items.Count * 2);
		}
	}
}

using System;

namespace Algorithms.Algorithm.EasySortings
{
	public class ESViewModel : ViewModel
	{
		private int viewElementStyle;

		public int ViewElementStyle
		{
			get { return viewElementStyle; }
			set
			{
				viewElementStyle = value;
				OnPropertyChanged("ViewElementStyle");
			}
		}

		public ESViewModel()
		{
			viewElementStyle = 1;
			Algorithm = new EasySortings();
		}
	}
}

using System;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BSViewModel : ViewModel
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

        public BSViewModel()
		{
			viewElementStyle = 1;
			Algorithm = new BinarySearch();
		}
	}
}

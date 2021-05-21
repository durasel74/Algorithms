using System;

namespace Algorithms.Algorithm.EasySortings
{
	public class ESViewModel : ViewModel
	{
		private int viewElementStyle;

		// Команда перемешивания массива
		private ButtonCommand shuffleCommnad;
		public ButtonCommand ShuffleCommand
		{
			get
			{
				return shuffleCommnad ??
					(shuffleCommnad = new ButtonCommand(obj =>
					{ Algorithm.Shuffle(); }));
			}
		}

		// Команда разворота массива
		private ButtonCommand reverseCommnad;
		public ButtonCommand ReverseCommand
		{
			get
			{
				return reverseCommnad ??
					(reverseCommnad = new ButtonCommand(obj =>
					{ Algorithm.Reverse(); }));
			}
		}

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

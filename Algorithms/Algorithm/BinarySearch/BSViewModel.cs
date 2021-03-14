using System;
using System.Windows;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BSViewModel : ViewModel
	{
        private BinarySearch test = new BinarySearch();

        public BinarySearch Test
        {
            get { return test; }
            set { }
		}

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        private ButtonCommand aplyCommand;
        public ButtonCommand AplyCommand
        {
            get
            {
                return aplyCommand ??
                      (aplyCommand = new ButtonCommand(obj =>
                      {
                          int count = 0;
                          if (Int32.TryParse(text, out count))
                          {
                              test.Count = count;
						  }
                      }));
            }
        }

        private int[] elements;
		public void CreateElements(string count)
		{
			int countElements;
			bool converted = int.TryParse(count, out countElements);
			if (converted)
				elements = new int[countElements];
		}
	}
}

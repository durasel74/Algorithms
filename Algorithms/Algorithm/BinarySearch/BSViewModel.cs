using System;
using System.Windows;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BSViewModel : ViewModel
	{
        private BinarySearch algorithm = new BinarySearch();

        public BSViewModel()
        {
            Play += algorithm.Start;
            algorithm.Count = 100;
		}

        public BinarySearch Algorithm
        {
            get { return algorithm; }
            set { }
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

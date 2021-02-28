using System;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BSViewModel : ViewModel
	{
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

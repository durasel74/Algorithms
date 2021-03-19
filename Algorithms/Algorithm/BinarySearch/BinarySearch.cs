using System;

namespace Algorithms.Algorithm.BinarySearch
{
	/// <summary>
	/// Алгоритм бинарного поиска в списке.
	/// </summary>
	public class BinarySearch : AlgorithmsProcessor
	{
		private int findElement;

		public int Low { get; private set; }
		public int High { get; private set; }
		public int ResultIndex { get; private set; }
		public int FindElement
		{ 
			get { return findElement; }
			set
			{
				findElement = value;
				OnPropertyChanged("FindElement");
			}
		}

		public BinarySearch()
		{
			Low = 0;
			High = Array.Count - 1;
			ResultIndex = -1;
			FindElement = 48;
			AlgorithmEventHandler += BinSearch;
			RestartEventHandler += RestartSearch;
		}

		private bool BinSearch()
		{
			int mid = (Low + High) / 2;
			SelectedElement = Array[mid];

			if (findElement == SelectedElement)
			{
				Low = mid + 1;
				High = mid - 1;
				ResultIndex = mid;
				return false;
			}
			else if (findElement > SelectedElement)
				Low = mid + 1;
			else if (findElement < SelectedElement)
				High = mid - 1;

			if (Low > High)
				return false;
			return true;
		}
		private void RestartSearch()
		{
			ResultIndex = -1;
			Low = 0;
			High = Array.Count - 1;
		}
    }
}

using System;

namespace Algorithms.Algorithm.BinarySearch
{
	/// <summary>
	/// Алгоритм бинарного поиска в списке.
	/// </summary>
	public class BinarySearch : AlgorithmsProcessor
	{
		private int requiredElement;

		public int Low { get; private set; }
		public int High { get; private set; }
		public int ResultIndex { get; private set; }
		public int RequiredElement
		{ 
			get { return requiredElement; }
			set
			{
				requiredElement = value;
				OnPropertyChanged("FindElement");
			}
		}

		public BinarySearch()
		{
			AlgorithmName = "Бинарный поиск";
			AlgorithmEventHandler += BinSearch;
			RestartEventHandler += RestartSearch;

			Low = 0;
			High = Array.Count - 1;
			ResultIndex = -1;
			RequiredElement = 48;
		}

		private bool BinSearch()
		{
			Attempt += 1;
			int mid = (Low + High) / 2;
			SelectedElement = Array[mid];

			if (requiredElement == SelectedElement)
			{
				Low = mid + 1;
				High = mid - 1;
				ResultIndex = mid;
				return false;
			}
			else if (requiredElement > SelectedElement)
				Low = mid + 1;
			else if (requiredElement < SelectedElement)
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

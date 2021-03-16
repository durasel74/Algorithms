using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;



using System.Windows;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BinarySearch : AlgorithmsProcessor
	{
		private int count = 0;
		private ObservableCollection<int> array = new ObservableCollection<int>();
		private int selectedNumber;
		private float speed = 1;
		private int findElement = 48;

		public bool IsStart { get; private set; }
		public int Low { get; private set; }
		public int High { get; private set; }
		public int Result { get; set; } = -1;
		public ObservableCollection<int> Array { get { return array; } set { } }
		public int Count
		{
			get	{ return count; }
			set
			{
				count = value;
				OnPropertyChanged("Count");
				FillArray(count);
			}
		}
		public int SelectedNumber
		{
			get { return selectedNumber; }
			set
			{
				selectedNumber = value;
				OnPropertyChanged("SelectedNumber");
			}
		}

		public async void Start()
		{
			IsStart = true;
			await Task.Run(() => StartSearch());
			IsStart = false;
		}

		public void StartSearch()
		{
			Low = 0;
			High = array.Count - 1;
			int mid;

			while (Low <= High)
			{
				mid = (Low + High) / 2;
				SelectedNumber = array[mid];

				if (findElement == selectedNumber)
				{
					Result = mid;
					return;
				}
				else if (findElement < selectedNumber)
					High = mid - 1;
				else if (findElement > selectedNumber)
					Low = mid + 1;
				Thread.Sleep((int)(1000 / speed));
			}
			return;
		}

		private void FillArray(int count)
		{
			array.Clear();
			for (int i = 1; i < count + 1; i++)
			{
				array.Add(i);
			}
			OnPropertyChanged("Array");
		}
    }
}

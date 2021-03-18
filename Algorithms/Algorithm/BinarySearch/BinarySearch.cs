using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;



using System.Windows;



namespace Algorithms.Algorithm.BinarySearch
{
	public class BinarySearch : AlgorithmsProcessor
	{
		private int findElement = 48;

		private ObservableCollection<int> array = new ObservableCollection<int>();
		private int count;
		private int selectedNumber;
		private static double speed = 1;

		private bool isComplite = true;
		private bool isRunning = false;

		public bool IsComplite { get { return isComplite; } }

		public bool IsRunning 
		{ 
			get { return isRunning; } 
			set
			{
				isRunning = value;
				OnPropertyChanged("IsRunning");
			}
		}

		public ObservableCollection<int> Array { get { return array; } set { } }

		public int Low { get; private set; }
		public int High { get; private set; }
		public int Result { get; set; } = -1;

		public int Count
		{
			get	{ return count; }
			set
			{
				count = value;
				OnPropertyChanged("Count");
				Result = -1;
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
		public double Speed
		{
			get { return speed; }
			set
			{
				if (value > 0 && value < 100)
				{
					double temp = Math.Round(value, 1);
					speed = temp;
					if (speed == 0)
						speed = 1;
					OnPropertyChanged("Speed");
				}
			}
		}

		public BinarySearch(int count)
		{
			Count = count;
			SelectedNumber = -1;
		}

		public void Close()
		{
			isComplite = true;
			IsRunning = false;
		}

		public async void Run()
		{
			isComplite = false;
			await Task.Run(() => StartSearch());
			IsRunning = false;
			isComplite = true;
		}

		private void StartSearch()
		{
			Low = 0;
			High = array.Count - 1;
			int mid;

			while (Low <= High && !isComplite)
			{
				//MessageBox.Show(isComplite.ToString());

				Thread.Sleep((int)(1000 / speed));

				if (!isRunning)
				{
					Thread.Sleep(100);
					continue;
				}

				mid = (Low + High) / 2;
				SelectedNumber = array[mid];

				if (findElement == selectedNumber)
				{
					Low = mid + 1;
					High = mid - 1;
					Result = mid;
					return;
				}
				else if (findElement < selectedNumber)
					High = mid - 1;
				else if (findElement > selectedNumber)
					Low = mid + 1;
			}
			return;
		}

		private void FillArray(int count)
		{
			array.Clear();
			for (int i = 1; i < count + 1; i++)
				array.Add(i);
			OnPropertyChanged("Array");
		}
    }
}

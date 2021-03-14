using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm.BinarySearch
{
	public class BinarySearch : AlgorithmsProcessor
	{
		private int count = 0;
		private ObservableCollection<int> array = new ObservableCollection<int>();
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

		public ObservableCollection<int> Array
		{
			get { return array; }
			set { }
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

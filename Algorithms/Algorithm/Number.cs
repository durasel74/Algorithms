using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
	public class Number : INotifyPropertyChanged
	{
		private int _value;

		public int Value 
		{ 
			get { return _value; }
			set
			{
				_value = value;
				OnPropertyChanged("Value");
			}
		
		}

		public Number(int value)
		{
			_value = value;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}

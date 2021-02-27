using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
	public class MainViewModel : INotifyPropertyChanged
	{



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

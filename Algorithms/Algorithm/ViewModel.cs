using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
	public class ViewModel : INotifyPropertyChanged
	{
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
                          
                      }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

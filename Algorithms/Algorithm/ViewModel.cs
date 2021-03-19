using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
    public class ViewModel : INotifyPropertyChanged
	{
        private AlgorithmsProcessor algorithm;

        private ButtonCommand startCommnad;
        private ButtonCommand restartCommand;

        public ButtonCommand StartCommand
        {
            get
            {
                return startCommnad ??
                    (startCommnad = new ButtonCommand(obj =>
                    {
                        algorithm.Run();
                    }));
            }
        }
        public ButtonCommand RestartCommand
        {
            get
            {
                return restartCommand ??
                    (restartCommand = new ButtonCommand(obj =>
                    {
                        algorithm.Close();
                        algorithm.RestartEvent();
                    }));
            }
        }

        public AlgorithmsProcessor Algorithm
        {
            get { return algorithm; }
            set
            {
                algorithm = value;
                OnPropertyChanged("Algorithm");
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

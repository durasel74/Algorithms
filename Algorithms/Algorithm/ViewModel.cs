using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
    public class ViewModel : INotifyPropertyChanged
	{
        private AlgorithmsProcessor algorithm;

        // Команда старта алгоритма
        private ButtonCommand startCommnad;
        public ButtonCommand StartCommand
        {
            get
            {
                return startCommnad ??
                    (startCommnad = new ButtonCommand(obj =>
                    { algorithm.Run(); }));
            }
        }

        // Команда перезапуска алгоритма
        private ButtonCommand restartCommand;
        public ButtonCommand RestartCommand
        {
            get
            {
                return restartCommand ??
                    (restartCommand = new ButtonCommand(obj =>
                    { algorithm.Restart(); }));
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

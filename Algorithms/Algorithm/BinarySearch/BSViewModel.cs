using System;




using System.Windows;




namespace Algorithms.Algorithm.BinarySearch
{
	public class BSViewModel : ViewModel
	{
        private BinarySearch algorithm = new BinarySearch(100);

        


        private ButtonCommand startCommnad;
        public ButtonCommand StartCommand
        {
            get
            {
                return startCommnad ??
                    (startCommnad = new ButtonCommand(obj =>
                    {
                        if (algorithm.IsComplite)
                        {
                            algorithm.Close();
                            Algorithm = new BinarySearch(100);
                            Algorithm.IsRunning = true;
                            Algorithm.Run();
						}
                    }));
            }
		}


        private ButtonCommand restartCommand;
        public ButtonCommand RestartCommand
        { 
            get
            {
                return restartCommand ??
                    (restartCommand = new ButtonCommand(obj =>
                    {
                        algorithm.Close();
                        Algorithm = new BinarySearch(100);
                    }));
			}
        }

        public BinarySearch Algorithm
        {
            get { return algorithm; }
            set
            {
                algorithm = value;
                OnPropertyChanged("Algorithm");
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms.Algorithm
{
    public class ViewModel : INotifyPropertyChanged
	{
        public delegate void PlayHandler();
        public event PlayHandler Play;

        private ButtonCommand mainButtonCommand;
        public ButtonCommand MainButtonCommand
        {
            get
            {
                return mainButtonCommand ??
                (mainButtonCommand = new ButtonCommand(obj =>
                {
                    string mainCommand = obj as string;
                    if (mainCommand != null)
                    {
                        switch (mainCommand)
                        {
                            case "Play":
                                Play.Invoke();
                                break;
                            case "Restart":
                                
                                break;
                        }
					}
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

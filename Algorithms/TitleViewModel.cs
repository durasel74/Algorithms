using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Algorithms
{
	public class TitleViewModel : INotifyPropertyChanged
	{
        private ButtonCommand openCommand;
        public ButtonCommand OpenCommand
        {
            get
            {
                return openCommand ??
                (openCommand = new ButtonCommand(obj =>
                {
                    string selectedAlgorithm = obj as string;
                    if (selectedAlgorithm != null)
                        OpenAlgorithm(selectedAlgorithm);
                }));
            }
        }

        private void OpenAlgorithm(string algorithm)
        {
            switch (algorithm)
            {
                case "ArraySearch":
                    var algorithmWindow = new Algorithm.BinarySearch.View();
                    algorithmWindow.Show();
                    break;
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

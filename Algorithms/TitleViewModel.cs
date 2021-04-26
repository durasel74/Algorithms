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
                    var SearchWindow = new Algorithm.BinarySearch.View();
                    SearchWindow.Show();
                    break;
                case "EasySortings":
                    var SortingsWindow = new Algorithm.EasySortings.View();
                    SortingsWindow.Show();
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
